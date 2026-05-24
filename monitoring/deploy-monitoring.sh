#!/bin/bash
# ═══════════════════════════════════════════════════════════════
#  deploy-monitoring.sh
#  Chạy trực tiếp trên Azure VM (sau khi SSH vào)
#  hoặc được gọi từ GitHub Action CD pipeline
# ═══════════════════════════════════════════════════════════════
set -e

GREEN='\033[0;32m'; YELLOW='\033[1;33m'; BLUE='\033[0;34m'; NC='\033[0m'
log_info()    { echo -e "${GREEN}[INFO]${NC} $1"; }
log_section() { echo -e "\n${BLUE}══ $1 ══${NC}"; }

# Fix k3s permission (giống cd.yml của Người 2)
sudo chmod 644 /etc/rancher/k3s/k3s.yaml || true

# Detect thư mục script đang chạy từ đâu
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

log_section "1. Namespace + RBAC"
kubectl apply -f k8s/00-namespace-rbac.yaml

log_section "2. ConfigMaps"
kubectl apply -f k8s/01-configmaps.yaml

# Grafana config riêng
kubectl create configmap grafana-datasources \
  --from-file=grafana/provisioning/datasources/datasources.yml \
  -n monitoring --dry-run=client -o yaml | kubectl apply -f -

kubectl create configmap grafana-dashboard-provisioning \
  --from-file=grafana/provisioning/dashboards/dashboards.yml \
  -n monitoring --dry-run=client -o yaml | kubectl apply -f -

kubectl create configmap grafana-dashboards \
  --from-file=grafana/dashboards/ \
  -n monitoring --dry-run=client -o yaml | kubectl apply -f -

log_section "3. Deployments (Prometheus, Grafana, Loki, Alertmanager)"
kubectl apply -f k8s/02-deployments.yaml

# Restart để nhận configmap mới nếu có thay đổi
kubectl rollout restart deployment/alertmanager -n monitoring 2>/dev/null || true
kubectl rollout restart deployment/grafana -n monitoring 2>/dev/null || true

log_section "4. DaemonSets (Node Exporter, Promtail)"
kubectl apply -f k8s/03-daemonsets.yaml

log_section "5. Ingress Grafana"
kubectl apply -f k8s/05-ingress.yaml

log_section "6. Chờ pods sẵn sàng..."
kubectl rollout status deployment/prometheus   -n monitoring --timeout=120s
kubectl rollout status deployment/grafana      -n monitoring --timeout=120s
kubectl rollout status deployment/loki         -n monitoring --timeout=120s
kubectl rollout status deployment/alertmanager -n monitoring --timeout=120s

log_section "7. Kết quả"
kubectl get pods -n monitoring
echo ""
echo "  Grafana: http://<IP-public>/grafana  (admin / DevOps@2024)"