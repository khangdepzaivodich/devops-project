BỘ GIÁO DỤC VÀ ĐÀO TẠO
TRƯỜNG ĐẠI HỌC SƯ PHẠM THÀNH PHỐ HỒ CHÍ MINH
KHOA CÔNG NGHỆ THÔNG TIN


# BÁO CÁO TIỂU LUẬN MÔN HỌC
**Môn học**: PHÁT TRIỂN VÀ VẬN HÀNH HỆ THỐNG (DEVOPS)

**ĐỀ TÀI**:
**XÂY DỰNG, TRIỂN KHAI VÀ TỰ ĐỘNG HÓA HỆ THỐNG THƯƠNG MẠI ĐIỆN TỬ FASHIONSTORE TRÊN NỀN TẢNG MICROSERVICES KẾT HỢP DOCKER, KUBERNETES, GITHUB ACTIONS CI/CD VÀ HỆ THỐNG GIÁM SÁT TẬP TRUNG (MONITORING & LOGGING)**

**Người thực hiện**: [Điền tên sinh viên]
**Mã số sinh viên**: [Điền MSSV]
**Giảng viên hướng dẫn**: [Điền tên Giảng viên]
**Năm học**: 2025 – 2026

*TP. HỒ CHÍ MINH, NĂM 2026*

---

## NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN

................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................

*TP. Hồ Chí Minh, ngày       tháng       năm 2026*
**Giảng viên hướng dẫn**
*(Ký và ghi rõ họ tên)*

---

## LỜI CAM ĐOAN

Tôi xin cam đoan đây là công trình nghiên cứu của riêng tôi. Các số liệu, kết quả nêu trong tiểu luận là trung thực và chưa được công bố trong bất kỳ công trình nào khác.

Tất cả các tài liệu tham khảo, đoạn trích dẫn đều được ghi rõ nguồn gốc. Nội dung nghiên cứu và kết quả trong đề tài này là trung thực và chưa từng được ai công bố trong bất cứ công trình nào.

*TP. Hồ Chí Minh, ngày       tháng       năm 2026*
**Tác giả tiểu luận**
*(Ký và ghi rõ họ tên)*

---

## LỜI CẢM ƠN

Để hoàn thành tiểu luận này, tôi xin bày tỏ lòng biết ơn sâu sắc đến Quý Thầy/Cô trong Khoa Công nghệ thông tin, Trường Đại học Sư phạm Thành phố Hồ Chí Minh đã tận tình giảng dạy, truyền đạt kiến thức và kỹ năng chuyên môn trong suốt quá trình học tập.

Đặc biệt, tôi xin gửi lời cảm ơn chân thành nhất đến Giảng viên hướng dẫn đã luôn tận tình chỉ bảo, định hướng và tạo mọi điều kiện thuận lợi để tôi hoàn thành đề tài này.

Cuối cùng, tôi xin cảm ơn gia đình, bạn bè và những người thân đã luôn quan tâm, động viên và hỗ trợ tôi trong suốt quá trình thực hiện tiểu luận.

Do kiến thức và kinh nghiệm còn hạn chế, tiểu luận không tránh khỏi những thiếu sót. Rất mong nhận được sự đóng góp ý kiến từ Quý Thầy/Cô để bản báo cáo được hoàn thiện hơn.

*TP. Hồ Chí Minh, năm 2026*
**Tác giả**

---

## TÓM TẮT

Tiểu luận này trình bày quá trình nghiên cứu, thiết kế và triển khai hệ thống thương mại điện tử FashionStore ứng dụng kiến trúc Microservices hiện đại kết hợp với phương pháp luận DevOps. Hệ thống bao gồm 6 dịch vụ backend độc lập được xây dựng bằng C# .NET 10, một giao diện người dùng Blazor WebAssembly và được vận hành toàn diện thông qua Docker, Kubernetes (K3s) và GitHub Actions CI/CD.

Nghiên cứu tập trung vào năm khía cạnh kỹ thuật cốt lõi: (1) Thiết kế kiến trúc phân rã dịch vụ theo miền nghiệp vụ; (2) Container hóa toàn bộ hệ thống với Docker sử dụng kỹ thuật Multi-stage Build; (3) Xây dựng quy trình CI/CD tự động hóa kiểm thử, phân tích chất lượng mã nguồn tĩnh bằng SonarCloud và đóng gói ảnh Docker lên Azure Container Registry; (4) Điều phối container và triển khai liên tục trên cụm Kubernetes K3s; (5) Tích hợp hệ thống giám sát hiệu năng (Monitoring) và ghi nhật ký tập trung (Centralized Logging) sử dụng Prometheus, Grafana, Loki, Promtail và Alertmanager.

Đồng thời, dự án cũng chú trọng vào chất lượng mã nguồn thông qua việc tái cấu trúc các dịch vụ (như ChatService) để tăng tính cô lập và khả năng kiểm thử đơn vị (Unit Testing) với xUnit, Moq, FluentAssertions, cũng như xây dựng bộ kiểm thử tích hợp (Integration Testing) toàn diện bằng WebApplicationFactory chạy in-memory để đạt độ bao phủ toàn diện cho hệ thống.

**Từ khóa**: *DevOps, Microservices, Docker, Kubernetes, CI/CD, GitHub Actions, SonarCloud, Blazor WebAssembly, Prometheus, Grafana, Loki, Unit Testing, Integration Testing, .NET 10.*

---

## MỤC LỤC

- [MỤC LỤC](#mục-lục)
- [DANH MỤC CÁC TỪ VIẾT TẮT](#danh-mục-các-từ-viết-tắt)
- [DANH MỤC HÌNH VẼ](#danh-mục-hình-vẽ)
- [DANH MỤC BẢNG BIỂU](#danh-mục-bảng-biểu)
- [MỞ ĐẦU](#mở-đầu)
  - [Lý do chọn đề tài và bối cảnh thực tiễn](#lý-do-chọn-đề-tài-và-bối-cảnh-thực-tiễn)
  - [Ý nghĩa của việc sử dụng DevOps](#ý-nghĩa-của-việc-sử-dụng-devops)
  - [Mục tiêu nghiên cứu và triển khai](#mục-tiêu-nghiên-cứu-và-triển-khai)
  - [Cấu trúc tiểu luận](#cấu-trúc-tiểu-luận)
- [CHƯƠNG 1: CƠ SỞ LÝ THUYẾT](#chương-1-cơ-sở-lý-thuyết)
  - [1.1. DevOps](#11-devops)
  - [1.2. GitHub Actions (Git Actions)](#12-github-actions-git-actions)
  - [1.3. Kubernetes](#13-kubernetes)
  - [1.4. SonarQube (SonarCloud)](#14-sonarqube-sonarcloud)
  - [1.5. Azure Container Registry (ACR)](#15-azure-container-registry-acr)
- [CHƯƠNG 2: KIẾN TRÚC HỆ THỐNG VÀ QUY TRÌNH DEVOPS](#chương-2-kiến-trúc-hệ-thống-và-quy-trình-devops)
  - [2.1. Kiến trúc tổng quan hệ thống Microservices](#21-kiến-trúc-tổng-quan-hệ-thống-microservices)
  - [2.2. Sơ đồ Source Control (Git Branching Strategy)](#22-sơ-đồ-source-control-git-branching-strategy)
  - [2.3. Sơ đồ CI Pipeline (Continuous Integration)](#23-sơ-đồ-ci-pipeline-continuous-integration)
    - [2.3.1. Backend CI](#231-backend-ci-dotnet-ciyml)
    - [2.3.2. Frontend CI](#232-frontend-ci-frontend-ciyml)
  - [2.4. Sơ đồ CD Pipeline (Continuous Deployment)](#24-sơ-đồ-cd-pipeline-continuous-deployment)
    - [2.4.1. Giai đoạn 1: Deploy Staging](#241-giai-đoạn-1-deploy-staging)
    - [2.4.2. Giai đoạn 2: Deploy Production](#242-giai-đoạn-2-deploy-production)
    - [2.4.3. Monitoring CD](#243-monitoring-cd-monitoring-cdyml)
  - [2.5. Deployment Environment](#25-deployment-environment)
- [CHƯƠNG 3: KIỂM THỬ VÀ ĐẢM BẢO CHẤT LƯỢNG](#chương-3-kiểm-thử-và-đảm-bảo-chất-lượng)
  - [3.1. Thiết kế và thực thi kiểm thử đơn vị (Unit Testing)](#31-thiết- kế-và-thực-thi-kiểm-thử-đơn-vị-unit-testing)
  - [3.2. Thiết kế và thực thi kiểm thử tích hợp (Integration Testing)](#32-thiết-kế-và-thực-thi-kiểm-thử-tích-hợp-integration-testing)
  - [3.3. Tái cấu trúc ChatService bằng Interface phục vụ Mocking](#33-tái-cấu-trúc-chatservice-bằng-interface-phục-vụ-mocking)
  - [3.4. Tự động hóa kiểm thử trong luồng CI](#34-tự-động-hóa-kiểm-thử-trong-luồng-ci)
  - [3.5. Phân tích chất lượng mã nguồn tự động với SonarCloud](#35-phân-tích-chất-lượng-mã-nguồn-tự-động-với-sonarcloud)
- [CHƯƠNG 4: TRIỂN KHAI VÀ VẬN HÀNH](#chương-4-triển-khai-và-vận-hành)
  - [4.1. Ảo hóa cấp độ Container với Docker](#41-ảo-hóa-cấp-độ-container-với-docker)
    - [4.1.1. Bản chất và vai trò của Docker](#411-bản-chất-và-vai-trò-of-docker)
    - [4.1.2. Phân tích Dockerfile cho Frontend (Multi-stage Build)](#412-phân-tích-dockerfile-cho-frontend-multi-stage-build)
    - [4.1.3. Khởi chạy cục bộ với Docker Compose](#413-khởi-chạy-cục-bộ-với-docker-compose)
  - [4.2. Vận hành Container với Kubernetes (K3s)](#42-vận-hành-container-với-kubernetes-k3s)
    - [4.2.1. Giới thiệu cụm K3s](#421-giới-thiệu-cụm-k3s)
    - [4.2.2. Cấu trúc tài nguyên Kubernetes](#422-cấu-trúc-tài-nguyên-kubernetes)
  - [4.3. Quy trình Triển khai liên tục (CD) tự động qua SSH](#43-quy-trình-triển-khai-liên-tục-cd-tự-động-qua-ssh)
    - [4.3.1. Tạo file cấu hình động từ GitHub Secrets](#431-tạo-file-cấu-hình-động-từ-github-secrets)
    - [4.3.2. Thực thi triển khai qua SSH](#432-thực-thi-triển-khai-qua-ssh)
  - [4.4. Giám sát hệ thống và Ghi nhật ký tập trung (Observability)](#44-giám-sát-hệ-thống-và-ghi-nhật-ký-tập-trung-observability)
    - [4.4.1. Thu thập metrics với Prometheus và Grafana](#441-thu-thập-metrics-với-prometheus-và-grafana)
    - [4.4.2. Quản lý log tập trung với Loki & Promtail và cảnh báo qua Alertmanager](#442-quản-lý-log-tập-trung-với-loki--promtail-và-cảnh-báo-qua-alertmanager)
- [KẾT LUẬN](#kết-luận)
- [TÀI LIỆU THAM KHẢO](#tài-liệu-tham-khảo)

---

## DANH MỤC CÁC TỪ VIẾT TẮT

Bảng 1.1 dưới đây giải thích định nghĩa các thuật ngữ và từ viết tắt cốt lõi được sử dụng trong suốt nội dung của báo cáo tiểu luận:

| Từ viết tắt | Giải thích |
| :--- | :--- |
| **CI/CD** | Continuous Integration / Continuous Delivery (Tích hợp liên tục / Triển khai liên tục) |
| **DevOps** | Development Operations (Quy trình phát triển và vận hành phần mềm tối ưu) |
| **API** | Application Programming Interface (Giao diện lập trình ứng dụng) |
| **JWT** | JSON Web Token (Mã thông báo xác thực gọn nhẹ dựa trên định dạng JSON) |
| **RBAC** | Role-based Access Control (Cơ chế kiểm soát truy cập dựa trên vai trò của người dùng) |
| **ACR** | Azure Container Registry (Kho lưu trữ hình ảnh Container bảo mật của Microsoft Azure) |
| **K3s** | Kubernetes phiên bản tối giản và tối ưu hóa tài nguyên từ Rancher |
| **SPA** | Single Page Application (Ứng dụng trang đơn tải động tài nguyên) |
| **SAST** | Static Application Security Testing (Công cụ quét lỗ hổng bảo mật mã nguồn tĩnh) |
| **ACID** | Atomicity, Consistency, Isolation, Durability (Tiêu chuẩn giao dịch cơ sở dữ liệu tin cậy) |
| **RAM** | Random Access Memory (Bộ nhớ truy cập ngẫu nhiên của thiết bị) |
| **CDN** | Content Delivery Network (Mạng lưới máy chủ phân phối nội dung tĩnh toàn cầu) |
| **VM** | Virtual Machine (Máy chủ ảo hóa độc lập) |
| **SSH** | Secure Shell (Giao thức mạng kết nối dòng lệnh mã hóa bảo mật từ xa) |
| **DDD** | Domain-Driven Design (Phương pháp thiết kế phần mềm hướng theo miền nghiệp vụ) |
| **Wasm** | WebAssembly (Định dạng mã nhị phân hiệu năng cao chạy trực tiếp trên trình duyệt) |
| **SMTP** | Simple Mail Transfer Protocol (Giao thức truyền tải thư điện tử) |
| **NoSQL** | Not only SQL (Cơ sở dữ liệu phi quan hệ lưu trữ dữ liệu dạng tài liệu hoặc phân tán) |

---

## DANH MỤC HÌNH VẼ

- **Hình 2.1**: Sơ đồ kiến trúc Microservices phi tập trung tổng thể của hệ thống
- **Hình 4.1**: Sơ đồ thiết kế luồng tự động hóa DevOps từ GitHub Repo đến cụm Kubernetes K3s

---

## DANH MỤC BẢNG BIỂU

- **Bảng 1.1**: Danh mục các từ viết tắt và định nghĩa giải thích chuyên ngành trong đề tài

---

## MỞ ĐẦU

### Lý do chọn đề tài và bối cảnh thực tiễn
Trong kỷ nguyên số hóa mạnh mẽ ngày nay, ngành thương mại điện tử (E-commerce) đòi hỏi các hệ thống phần mềm phải có khả năng vận hành liên tục 24/7, chịu tải cao, phản hồi nhanh chóng và cập nhật tính năng mới liên tục mà không gây gián đoạn dịch vụ. Mô hình kiến trúc nguyên khối (Monolith) truyền thống dần bộc lộ những hạn chế nghiêm trọng như: khó mở rộng cục bộ, rủi ro sập toàn bộ hệ thống khi một module nhỏ gặp lỗi, thời gian build và deploy kéo dài, khó khăn trong việc áp dụng nhiều công nghệ khác nhau trên cùng một mã nguồn.

Để giải quyết triệt để vấn đề này, kiến trúc vi dịch vụ (Microservices) nổi lên như một giải pháp hàng đầu. Bằng cách chia nhỏ hệ thống thành các dịch vụ độc lập, chạy riêng biệt và giao tiếp qua các giao thức mạng chuẩn hóa, doanh nghiệp có thể tăng tốc độ phát triển và tối ưu hóa tài nguyên phần cứng. Tuy nhiên, việc vận hành hàng chục microservices khác nhau lại đặt ra thách thức cực kỳ lớn về triển khai, quản lý cấu hình, kiểm thử chất lượng và đồng bộ hóa môi trường giữa các nhóm phát triển và vận hành. Đây chính là lý do phương pháp luận DevOps và các công cụ tự động hóa đi kèm trở thành yếu tố bắt buộc để dự án thành công.

### Ý nghĩa của việc sử dụng DevOps
Việc triển khai quy trình DevOps trong dự án mang lại những lợi ích thiết thực:
- **Rút ngắn thời gian đưa sản phẩm ra thị trường (Time-to-Market)**: Nhờ có các pipeline CI/CD tự động chạy khi có code mới, quá trình tích hợp mã nguồn, build và deploy lên môi trường staging/production chỉ diễn ra trong vài phút thay vì hàng giờ làm tay.
- **Tăng cường chất lượng và tính ổn định**: Việc tích hợp bộ quét tĩnh SonarCloud giúp phát hiện sớm các lỗ hổng bảo mật nghiêm trọng (như Open Redirect, SQL Injection), các đoạn code thừa, code có độ phức tạp cao ngay trong pull request, tránh đưa lỗi lên môi trường thực tế.
- **Tối ưu hóa chi phí hạ tầng**: Docker và Kubernetes giúp cô lập ứng dụng và tận dụng tối đa tài nguyên máy chủ. Kubernetes quản lý trạng thái của các pod một cách tự động, tự khởi động lại pod hỏng và tự điều hướng lưu lượng traffic phù hợp.

### Mục tiêu nghiên cứu và triển khai
Đề tài hướng tới nghiên cứu và triển khai thực tế một hệ thống thương mại điện tử bằng DevOps với các quy trình CI/CD chặt chẽ:
- Xây dựng môi trường ảo hóa container hóa hoàn chỉnh cho tất cả các dịch vụ bằng Docker.
- Thiết lập hệ thống CI/CD tự động kiểm thử đơn vị, kiểm thử tích hợp, phân tích bảo mật tĩnh (SAST), đo đạc chỉ số kỹ thuật mã nguồn (SonarCloud Quality Gate) và tự động đóng gói, phát hành sản phẩm.
- Triển khai và điều phối hạ tầng trên cụm Kubernetes nhằm tối ưu hóa khả năng tự phục hồi (Self-healing), cân bằng tải tự động (Load balancing) và nâng cấp hệ thống không gián đoạn (Zero-downtime deployment).

### Cấu trúc tiểu luận
Nội dung tiểu luận sẽ được trình bày theo cấu trúc sau:
- **Chương 1**: Cơ sở lý thuyết
- **Chương 2**: Kiến trúc hệ thống
- **Chương 3**: Kiểm thử và đảm bảo chất lượng
- **Chương 4**: Triển khai và vận hành

---

## CHƯƠNG 1: CƠ SỞ LÝ THUYẾT

### 1.1. DevOps
DevOps không đơn thuần là sự kết hợp giữa các công cụ phần mềm, mà là một cuộc cách mạng về văn hóa cộng tác giữa hai phòng ban phát triển (Development) và vận hành (Operations). DevOps thúc đẩy văn hóa chia sẻ trách nhiệm, tăng cường tự động hóa ở mọi khâu của vòng đời phát triển phần mềm (SDLC). Mục tiêu tối thượng của DevOps là thu hẹp khoảng cách giữa các nhóm, tối ưu hóa tốc độ phân phối tính năng và đảm bảo hạ tầng hoạt động với độ sẵn sàng cao nhất, tỷ lệ phát sinh lỗi sau phát hành tiệm cận mức tối thiểu.

### 1.2. GitHub Actions (Git Actions)
GitHub Actions là giải pháp tự động hóa tích hợp sẵn của GitHub, cho phép các nhóm phát triển định nghĩa quy trình làm việc (Workflows) tự động dưới dạng tệp tin YAML. Hệ thống hoạt động theo kiến trúc hướng sự kiện (Event-driven API), kích hoạt luồng CI/CD mỗi khi có sự kiện đẩy mã nguồn (`push`), tạo yêu cầu tích hợp (`pull_request`) hay phát hành phiên bản (`release`). GitHub Actions cung cấp môi trường ảo hóa đa dạng (Ubuntu Linux, Windows Server, macOS) đóng vai trò các máy thực thi (Runner), giúp biên dịch mã nguồn, chạy kiểm thử tự động và đóng gói Docker image trực tiếp từ kho chứa.

### 1.3. Kubernetes
Kubernetes (K8s) là nền tảng điều phối và quản lý container mã nguồn mở hàng đầu thế giới. Kubernetes điều phối tập hợp các máy chủ vật lý hoặc máy chủ ảo (Node) thành một cụm duy nhất (Cluster) để chạy container ứng dụng. Các tính năng cốt lõi của Kubernetes bao gồm:
- **Định tuyến và Phát hiện Dịch vụ (Service Discovery)**: Tự động gắn IP nội bộ và DNS cho container để chúng giao tiếp thuận tiện.
- **Cân bằng tải (Load Balancing)**: Tự động phân phối lượng truy cập đồng đều đến các bản sao container.
- **Tự khôi phục (Self-healing)**: Tự động phát hiện pod gặp sự cố, tiêu hủy và khởi động lại pod mới ở trạng thái sạch.
- **Triển khai tự động (Rolling Update)**: Cho phép nâng cấp mã nguồn phần mềm dần dần mà không gây mất kết nối của khách hàng (Zero-downtime).

### 1.4. SonarQube (SonarCloud)
SonarQube và phiên bản đám mây SonarCloud là những bộ phân tích chất lượng mã nguồn tĩnh hàng đầu. SonarCloud thực hiện phân tích cú pháp mã nguồn nhằm phát hiện lỗi tiềm ẩn (Bugs), lỗ hổng bảo mật (Vulnerabilities/Hotspots), và các điểm mã nguồn chưa tối ưu (Code Smells). Nó cung cấp chỉ số đánh giá độ bao phủ kiểm thử (Code Coverage) và thiết lập các cổng chất lượng (Quality Gates). Nếu mã nguồn không vượt qua Quality Gate đã cấu hình (ví dụ: phát hiện lỗ hổng bảo mật nghiêm trọng hoặc độ bao phủ kiểm thử dưới 80%), quy trình CI sẽ lập tức dừng lại để ngăn chặn lỗi đi vào môi trường thực tế.

### 1.5. Azure Container Registry (ACR)
Azure Container Registry (ACR) là dịch vụ lưu trữ và quản lý Docker Images riêng tư (Private) chạy trên nền tảng đám mây Microsoft Azure. ACR đóng vai trò là cầu nối giữa pha CI (GitHub Actions) và pha CD (Kubernetes). Sau khi GitHub Actions biên dịch và đóng gói thành công các Docker Images cho frontend và 6 microservices backend, các ảnh này được đẩy trực tiếp lên kho chứa bảo mật của ACR. Cụm Kubernetes ở máy chủ deployment sau đó sẽ sử dụng tài khoản bí mật (Image Pull Secret) để kéo (pull) các ảnh mới nhất về và chạy trên các nút của cụm.

---

## CHƯƠNG 2: KIẾN TRÚC HỆ THỐNG VÀ QUY TRÌNH DEVOPS

### 2.1. Kiến trúc tổng quan hệ thống Microservices
Hệ thống FashionStore được xây dựng theo mô hình kiến trúc hướng dịch vụ phi tập trung (Decentralized Microservices Architecture). Phía Frontend gửi các yêu cầu HTTP/HTTPS thông qua cơ chế định tuyến Ingress của Kubernetes để phân phối trực tiếp đến các dịch vụ xử lý nghiệp vụ chuyên biệt.

Mỗi dịch vụ sở hữu cơ sở dữ liệu riêng biệt theo mẫu Database-per-Service, ngăn chặn việc một dịch vụ truy cập trực tiếp vào cơ sở dữ liệu của dịch vụ khác. Giao tiếp giữa các dịch vụ được thực hiện thông qua API RESTful dựa trên HTTP, kết hợp với kết nối thời gian thực qua WebSockets (SignalR) cho tính năng live chat.

Hệ thống bao gồm các thành phần sau:

**Frontend — Blazor WebAssembly (.NET 10)**:
- Ứng dụng SPA chạy trực tiếp mã C# client-side trên trình duyệt thông qua biên dịch WebAssembly.
- Tailwind CSS v4 (LightningCSS) phục vụ thiết kế giao diện responsive.
- SignalR Client hỗ trợ kết nối WebSockets hai chiều thời gian thực.

**Backend — 6 Microservices độc lập viết bằng C# .NET 10**:

| Dịch vụ | Cổng ngoài | Chức năng chính | Cơ sở dữ liệu |
| :--- | :---: | :--- | :--- |
| **IdentityService** | 7093 | Đăng ký, đăng nhập, cấp JWT (RSA), phân quyền RBAC | SQL Server, Cloudinary, SMTP |
| **CatalogService** | 7103 | Quản lý danh mục, sản phẩm, tồn kho, tìm kiếm, slug SEO | SQL Server, Cloudinary |
| **OrderingService** | 7076 | Đặt hàng, áp dụng coupon, lưu hóa đơn, cập nhật trạng thái | SQL Server |
| **BasketService** | 7021 | Giỏ hàng tạm thời, gộp giỏ khách vãng lai sau đăng nhập | Redis Cache |
| **DiscountService** | 7002 | Quản lý coupon giảm giá (% hoặc cố định), giới hạn đối tượng | MongoDB |
| **ChatService** | 7229 | Phiên chat trực tuyến, hàng đợi, phân phối tin nhắn Staff | MongoDB, Redis Cache |

**Hạ tầng dữ liệu và dịch vụ bên thứ ba**:
- **SQL Server**: Lưu trữ dữ liệu có cấu trúc (Identity, Catalog, Ordering), tuân thủ chuẩn ACID.
- **MongoDB**: Lưu trữ phi cấu trúc cho ChatService và DiscountService.
- **Redis Cache**: Lưu giỏ hàng tạm thời (BasketService) và trạng thái trực tuyến (ChatService), phản hồi dưới 5ms.
- **Cloudinary API**: Tải lên và phân phối hình ảnh sản phẩm qua CDN toàn cầu.
- **SMTP Server**: Gửi email giao dịch, đăng ký tài khoản và khôi phục mật khẩu.

### 2.2. Sơ đồ Source Control (Git Branching Strategy)
Dự án áp dụng chiến lược phân nhánh Git dựa trên nhánh chính `main` (hoặc `master`) kết hợp với nhánh tính năng (Feature Branch) và cơ chế Pull Request:

**Quy trình làm việc**:
1. Lập trình viên tạo nhánh `feature/xxx` từ nhánh `main` để phát triển tính năng mới hoặc sửa lỗi.
2. Sau khi hoàn thành, tạo Pull Request (PR) yêu cầu tích hợp vào nhánh `main`.
3. GitHub Actions CI tự động kích hoạt trên PR: biên dịch, chạy kiểm thử, quét SonarCloud.
4. Nếu CI thành công và được review, PR được merge vào `main`.
5. Sự kiện `push` vào `main` tự động kích hoạt pipeline CD triển khai lên Staging → Production.

**Cấu hình kích hoạt**:
- Pipeline CI kích hoạt trên sự kiện `push` và `pull_request` vào nhánh `master` hoặc `main`.
- Pipeline Backend CI chỉ chạy khi có thay đổi trong thư mục `serious/**`.
- Pipeline Frontend CI chỉ chạy khi có thay đổi trong thư mục `blazor_frontend/**`.
- Cơ chế `paths` filter giúp tiết kiệm tài nguyên Runner, chỉ build lại phần mã nguồn thực sự thay đổi.

### 2.3. Sơ đồ CI Pipeline (Continuous Integration)
Hệ thống sử dụng hai pipeline CI song song trên GitHub Actions, mỗi pipeline phục vụ một phần của hệ thống:

#### 2.3.1. Backend CI (`dotnet-ci.yml`)
Pipeline xử lý toàn bộ 6 microservices backend, bao gồm các bước tuần tự:

| Bước | Tên | Mô tả |
| :---: | :--- | :--- |
| 1 | **Checkout** | Tải mã nguồn từ GitHub repository |
| 2 | **Setup JDK 17 + .NET 10** | Cài đặt môi trường biên dịch (JDK cho SonarScanner, .NET cho ứng dụng) |
| 3 | **Restore dependencies** | Khôi phục các gói NuGet từ file `serious.slnx` |
| 4 | **Begin SonarCloud Analysis** | Khởi tạo phiên quét tĩnh với project key, token và cấu hình exclusions |
| 5 | **Build** | Biên dịch toàn bộ solution |
| 6 | **Test** | Chạy `dotnet test` với thu thập Code Coverage (OpenCover format) |
| 7 | **End SonarCloud Analysis** | Gửi kết quả phân tích lên SonarCloud, kiểm tra Quality Gate |
| 8 | **Docker Build & Push** | Duyệt vòng lặp tất cả thư mục `Services/*/` có Dockerfile, chuyển tên PascalCase sang kebab-case, build và push image lên ACR |
| 9 | **Discord Notification** | Thông báo trạng thái build (thành công/thất bại) qua webhook Discord |

#### 2.3.2. Frontend CI (`frontend-ci.yml`)
Pipeline xử lý ứng dụng Blazor WebAssembly, bao gồm các bước tuần tự:

| Bước | Tên | Mô tả |
| :---: | :--- | :--- |
| 1 | **Checkout** | Tải mã nguồn từ GitHub repository |
| 2 | **Setup JDK 17 + Node.js 20 + .NET 10** | Cài đặt môi trường (Node.js cho Tailwind CSS build) |
| 3 | **Install npm dependencies** | Chạy `npm ci` để cài đặt các gói Node.js (Tailwind CSS v4) |
| 4 | **Restore dependencies** | Khôi phục gói NuGet từ `blazor_frontend.slnx` |
| 5 | **Begin SonarCloud Analysis** | Khởi tạo phiên quét tĩnh, loại trừ `wwwroot/lib/**` và file JS bên thứ ba, loại trừ toàn bộ coverage (`sonar.coverage.exclusions=**/*`) |
| 6 | **Build** | Biên dịch solution frontend |
| 7 | **End SonarCloud Analysis** | Gửi kết quả phân tích lên SonarCloud |
| 8 | **Docker Build & Push** | Build image frontend bằng `Dockerfile.frontend` (Multi-stage: .NET SDK → Nginx Alpine) và push lên ACR |
| 9 | **Discord Notification** | Thông báo trạng thái build qua webhook Discord |

### 2.4. Sơ đồ CD Pipeline (Continuous Deployment)
Pipeline CD (`cd.yml`) được kích hoạt tự động sau khi pipeline CI hoàn thành thành công, sử dụng cơ chế `workflow_run`. Pipeline triển khai qua hai giai đoạn tuần tự:

#### 2.4.1. Giai đoạn 1: Deploy Staging
| Bước | Tên | Mô tả |
| :---: | :--- | :--- |
| 1 | **Generate .env file** | Đọc 20+ GitHub Secrets (DB connections, JWT keys, SMTP, Cloudinary...) và ghi ra tệp `.env.deploy` |
| 2 | **SCP to Azure VM** | Truyền tải thư mục `k8s/` và `.env.deploy` lên máy chủ ảo qua SSH |
| 3 | **Create namespace** | Tạo namespace `staging` nếu chưa tồn tại |
| 4 | **Create ACR Secret** | Tạo `docker-registry` secret để K3s có thể pull image từ ACR |
| 5 | **Replace placeholders** | Thay thế `__ACR_LOGIN_SERVER__` trong các file YAML bằng địa chỉ ACR thực tế |
| 6 | **Apply manifests** | Áp dụng ConfigMaps, Secrets, Backend, Frontend, Ingress vào namespace `staging` |
| 7 | **Rolling Update** | Restart tất cả 7 deployments (6 backend + 1 frontend) và chờ rollout hoàn tất |
| 8 | **Cleanup** | Xóa tệp `.env.deploy` trên server để bảo mật |

#### 2.4.2. Giai đoạn 2: Deploy Production
- Yêu cầu **Manual Approval** từ người quản lý qua GitHub Environments (cấu hình `environment: production`).
- Thực hiện các bước tương tự Deploy Staging nhưng trên namespace `production`.
- Hỗ trợ **Rollback** thông qua `workflow_dispatch` với tùy chọn `rollback`, thực thi lệnh `kubectl rollout undo` cho tất cả deployments.

#### 2.4.3. Monitoring CD (`monitoring-cd.yml`)
Pipeline triển khai riêng cho hạ tầng giám sát (Prometheus, Grafana, Loki, Alertmanager):
- Kích hoạt khi có thay đổi trong thư mục `monitoring/**`.
- Triển khai tuần tự: Staging → Production (có manual approval).
- Tự động inject Discord webhook và Grafana credentials từ GitHub Secrets vào ConfigMaps.

### 2.5. Deployment Environment
Toàn bộ hệ thống được triển khai trên máy chủ ảo Azure VM chạy cụm Kubernetes K3s — bản phân phối Kubernetes siêu nhẹ do Rancher phát triển, chỉ sử dụng khoảng 512MB RAM.

**Cấu trúc môi trường triển khai**:

| Thành phần | Mô tả |
| :--- | :--- |
| **Azure VM** | Máy chủ ảo chạy K3s cluster, kết nối SSH từ GitHub Actions |
| **K3s Cluster** | Kubernetes tối giản, cung cấp đầy đủ tính năng: Service Discovery, Load Balancing, Self-healing, Rolling Update |
| **Namespace `staging`** | Môi trường kiểm thử trước khi đưa lên production |
| **Namespace `production`** | Môi trường vận hành thực tế phục vụ người dùng cuối |
| **Namespace `monitoring`** | Hạ tầng giám sát: Prometheus, Grafana, Loki, Promtail, Alertmanager |
| **Azure Container Registry** | Kho chứa Docker Images riêng tư, cầu nối giữa CI và CD |

**Tài nguyên Kubernetes được sử dụng**:
- **ConfigMaps** (`k8s/config/`): Biến cấu hình không nhạy cảm (URL nội bộ microservice, ASPNETCORE_ENVIRONMENT).
- **Secrets**: Thông tin nhạy cảm mã hóa Base64 (chuỗi kết nối DB, khóa JWT, API keys).
- **Deployments** (`k8s/backend/`, `k8s/frontend/`): Quản lý replica, tự động khởi động lại container bị sập, chiến lược Rolling Update.
- **Services**: Cung cấp IP nội bộ và DNS ổn định cho pods (ví dụ: `http://catalog-service:8080`), cân bằng tải nội bộ.
- **Ingress** (`k8s/ingress/`): Định tuyến HTTPS từ Internet vào các Service dựa trên đường dẫn (Path-based routing).

---

## CHƯƠNG 3: KIỂM THỬ VÀ ĐẢM BẢO CHẤT LƯỢNG

Để đảm bảo hệ thống microservices vận hành bền bỉ và không phát sinh lỗi logic trong quá trình cập nhật mã nguồn liên tục, dự án đã xây dựng một chiến lược kiểm thử tự động đa tầng kết hợp phân tích chất lượng tĩnh chặt chẽ.

### 3.1. Thiết kế và thực thi kiểm thử đơn vị (Unit Testing)
Mỗi dịch vụ microservice đều có một dự án unit test đi kèm (ví dụ: `BasketService.Tests`, `ChatService.Tests`, `DiscountService.Tests`, `OrderingService.Tests`), nâng tổng số unit test chạy trên CI lên **83 test cases**. Các công cụ hỗ trợ chính bao gồm:
- **xUnit**: Cung cấp khung kiểm thử độc lập chạy các test case song song để tăng tốc độ phản hồi.
- **Moq**: Giả lập (Mock) hành vi của các phụ thuộc dịch vụ ngoài (như Mail service, Cloudinary) và cô lập logic của các thành phần cần kiểm định.
- **FluentAssertions**: Cấu trúc các điều kiện kiểm tra dữ liệu trực quan bằng tiếng Anh tự nhiên (ví dụ: `result.Should().BeOfType<OkObjectResult>()`), giúp dễ bảo trì và phân tích nguyên nhân lỗi.

Các thành phần quan trọng đã được kiểm thử:
- **BasketControllerTests (10 tests)**: Kiểm thử giỏ hàng, cập nhật số lượng, xóa sản phẩm và phân quyền admin/user.
- **ChatControllerTests & ChatHubTests (22 tests)**: Kiểm định API truy vấn phòng chat, hành vi kết nối của khách hàng, phân phối tin nhắn của nhân viên và các sự kiện kết nối thời gian thực trên Hub.
- **ChatCleanupWorkerTests (1 test)**: Kiểm định worker chạy ngầm tự động quét và thu hồi các phiên chat bị bỏ quên quá hạn.
- **MaGiamGiaControllerTests (14 tests)**: Kiểm định đầy đủ các hành vi CRUD và áp dụng coupon giảm giá.
- **DonHangControllerTests (17 tests)**: Kiểm định luồng tạo đơn hàng, kiểm tra tính hợp lệ của số lượng tồn kho và thông tin giao dịch.

### 3.2. Thiết kế và thực thi kiểm thử tích hợp (Integration Testing)
Để kiểm định hoạt động đồng bộ của các dịch vụ ở cấp độ API hoàn chỉnh, chúng tôi đã xây dựng dự án [IntegrationTests](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/Services/IntegrationTests/IntegrationTests.csproj) thực thi **9 ca kiểm thử tích hợp** với các chiến lược sau:
- **Môi trường chạy In-Memory hoàn toàn**: Quy trình kiểm thử tích hợp không yêu cầu bất kỳ hạ tầng vật lý nào (như SQL Server, MongoDB hay Redis thật). Mọi cơ sở dữ liệu quan hệ được thay thế bằng `Microsoft.EntityFrameworkCore.InMemory` khi biến cấu hình `UseInMemoryDatabase` được thiết lập bằng `"true"`.
- **Cấu hình an toàn cho JWT Authorization**: Sử dụng lớp hỗ trợ [TestJwtSettings](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/Services/IntegrationTests/TestJwtSettings.cs) để sinh khóa RSA ngẫu nhiên và cấu hình nó cho Middleware xác thực của máy chủ ảo. Điều này cho phép tạo ra các mã JWT Bearer hợp lệ để kiểm thử trực tiếp các API bảo mật được gán nhãn `[Authorize]` (như Basket và Ordering).
- **Mocking các Dịch vụ Phức tạp**: Sử dụng Moq để thay thế các lớp giao tiếp vật lý MongoDB/Redis và đăng ký chúng trực tiếp vào IoC Container thông qua `WebApplicationFactory.WithWebHostBuilder()`.

Kết quả kiểm thử tích hợp xác nhận tất cả 6 dịch vụ đều khởi động, định tuyến API và phản hồi thành công:
- `IdentityIntegrationTests` (2 tests): Đăng nhập và đăng ký tài khoản thành công, trả về trạng thái JSON chính xác.
- `CatalogIntegrationTests` (3 tests): Seed dữ liệu danh mục ban đầu và kiểm định API lấy danh mục, loại sản phẩm và sản phẩm hoạt động trơn tru.
- `OrderingIntegrationTests` (1 test): Kiểm định tạo đơn hàng qua cổng an toàn JWT.
- `BasketIntegrationTests` (1 test): Kiểm định giỏ hàng qua JWT Bearer.
- `DiscountIntegrationTests` (1 test) & `ChatIntegrationTests` (1 test): Kiểm định các cổng gọi API trung gian.

### 3.3. Tái cấu trúc ChatService bằng Interface phục vụ Mocking
Ban đầu, `ChatController` và `ChatHub` trong `ChatService` phụ thuộc trực tiếp vào các lớp dịch vụ cụ thể `ChatMongoService` và `ChatRedisService` (chứa logic kết nối trực tiếp đến cơ sở dữ liệu MongoDB và Redis thực tế). Điều này khiến việc viết unit test không khả thi vì kiểm thử đơn vị yêu cầu không phụ thuộc vào hạ tầng vật lý.

Giải pháp tái cấu trúc đã được thực hiện:
- Trích xuất hai interface [IChatMongoService](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/Services/ChatService/ChatAPI/Services/Interfaces/IChatMongoService.cs) và [IChatRedisService](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/Services/ChatService/ChatAPI/Services/Interfaces/IChatRedisService.cs).
- Cho các lớp dịch vụ thực tế implement các interface này.
- Cập nhật constructor của `ChatController`, `ChatHub` và `ChatCleanupWorker` để yêu cầu interface thông qua Dependency Injection.
- Đăng ký các interface này trong IoC Container của ASP.NET Core (`Program.cs`) dưới dạng Singleton.

Nhờ đó, trong dự án test, ta có thể dễ dàng sử dụng Moq để giả lập dữ liệu trả về của MongoDB và Redis mà không cần khởi chạy cơ sở dữ liệu thật.

### 3.4. Tự động hóa kiểm thử trong luồng CI
Tất cả các dự án kiểm thử được tích hợp vào tệp giải pháp chung [serious.slnx](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/serious.slnx). Khi lập trình viên tạo pull request, GitHub Actions CI sẽ tự động kích hoạt tiến trình và chạy lệnh `dotnet test`. Nếu có bất kỳ test case nào thất bại, tiến trình sẽ báo lỗi đỏ, khóa hành động Merge và ngăn chặn đóng gói phát hành ứng dụng lên Azure Container Registry.

### 3.5. Phân tích chất lượng mã nguồn tự động với SonarCloud
SonarCloud được cấu hình làm cổng kiểm duyệt an toàn (Quality Gate) trong pha CI của GitHub Actions. Dưới đây là kết quả phân tích chất lượng mã nguồn thực tế được trích xuất trực tiếp từ SonarCloud cho cả hai dự án Backend và Frontend:

#### 3.5.1. Tổng quan Quality Gate

| Dự án | Quality Gate | Reliability (Bugs) | Security (Vulnerabilities) | Maintainability (Code Smells) | Dòng mã (LoC) |
| :--- | :---: | :---: | :---: | :---: | :---: |
| **Serious Backend** | ✅ Passed | A (0 Bugs) | C (2 Vulnerabilities) | A (196 Code Smells) | 5.665 |
| **Blazor Frontend** | ✅ Passed | A (0 Bugs) | B (1 Vulnerability) | A (123 Code Smells) | 13.575 |

#### 3.5.2. Chi tiết chỉ số kỹ thuật

**Bảng 3.1: Kết quả phân tích SonarCloud — Backend Microservices**

| Chỉ số (Metric) | Giá trị | Đánh giá |
| :--- | :---: | :--- |
| Quality Gate Status | **Passed** | Vượt qua toàn bộ ngưỡng kiểm duyệt tự động |
| Bugs | **0** | Không phát hiện lỗi logic tiềm ẩn (Rating: A) |
| Vulnerabilities | **2** | Tồn tại 2 cảnh báo bảo mật cần xem xét (Rating: C) |
| Security Hotspots | **33** | 33 điểm mã nguồn nhạy cảm cần review thủ công |
| Code Smells | **196** | Các đoạn mã chưa tối ưu nhưng không gây lỗi (Rating: A) |
| Code Coverage | **30.6%** | Độ bao phủ kiểm thử đơn vị trên mã nguồn backend |
| Duplicated Lines | **1.4%** | Tỷ lệ trùng lặp mã nguồn thấp |
| Cognitive Complexity | **504** | Tổng độ phức tạp nhận thức toàn bộ dự án |
| Lines of Code | **5.665** | Tổng số dòng mã nguồn hiệu dụng (không tính comment/blank) |

**Bảng 3.2: Kết quả phân tích SonarCloud — Blazor Frontend**

| Chỉ số (Metric) | Giá trị | Đánh giá |
| :--- | :---: | :--- |
| Quality Gate Status | **Passed** | Vượt qua toàn bộ ngưỡng kiểm duyệt tự động |
| Bugs | **0** | Không phát hiện lỗi logic tiềm ẩn (Rating: A) |
| Vulnerabilities | **1** | 1 cảnh báo bảo mật (Rating: B) |
| Security Hotspots | **4** | 4 điểm mã nguồn nhạy cảm cần review thủ công |
| Code Smells | **123** | Các đoạn mã chưa tối ưu nhưng không gây lỗi (Rating: A) |
| Code Coverage | **N/A** | Được loại trừ khỏi yêu cầu đo coverage (cấu hình `sonar.coverage.exclusions`) |
| Duplicated Lines | **0.0%** | Không có mã nguồn trùng lặp |
| Cognitive Complexity | **1.893** | Tổng độ phức tạp nhận thức toàn bộ dự án |
| Lines of Code | **13.575** | Tổng số dòng mã nguồn hiệu dụng (không tính comment/blank) |

#### 3.5.3. Các vấn đề đã phát hiện và giải quyết nhờ SonarCloud
Trong quá trình tích hợp SonarCloud vào luồng CI, các vấn đề sau đã được phát hiện và xử lý:
- **Vấn đề bảo mật Open Redirect (S5146)**: SonarCloud cảnh báo redirect trực tiếp dựa trên tham số query `returnUrl` chưa kiểm duyệt có thể bị hacker khai thác dẫn dụ người dùng sang trang web độc hại (Phishing). Giải pháp: Xây dựng hàm `GetSafeReturnUrl()` chỉ chấp nhận URL tương đối nội bộ bắt đầu bằng dấu gạch chéo `/`.
- **Tối ưu hóa hiệu năng Collection Copying (S2933 / S2325)**: Sửa các thuộc tính C# trả về danh sách dạng `.ToList()` trong Property Getter gây cấp phát bộ nhớ liên tục bằng cách refactor thành các phương thức hỗ trợ tường minh (`GetDisplayProducts()`).
- **Giảm Cognitive Complexity**: Chia nhỏ các hàm khởi tạo phức tạp (độ phức tạp nhận thức > 25) thành các hàm helper nhỏ hơn, đảm bảo mã nguồn tuân thủ giới hạn độ phức tạp nhận thức của SonarCloud (< 15).
- **Cấu hình loại trừ quét**: Cấu hình `sonar.exclusions` để loại bỏ các thư mục `bin/`, `obj/`, các file thư viện JavaScript của bên thứ ba khỏi phân tích tĩnh để tránh báo lỗi giả (false positives). Loại trừ phần frontend khỏi yêu cầu đo độ phủ để bảo vệ tính nhất quán của Quality Gate.

---

## CHƯƠNG 3: TRIỂN KHAI VÀ VẬN HÀNH

*Hình 4.1 dưới đây thể hiện sơ đồ luồng DevOps CI/CD của dự án:*
```
[ Lập trình viên ]
       │ Push Code
       ▼
 [ GitHub Repo ] ──(Trigger)──► [ GitHub Actions Runner ]
                                           │
                             ┌─────────────┴──────────────┐
                             │             │              │
                       Bước 1:         Bước 2:        Bước 3:
                    Restore & Build  SonarCloud    Docker Build
                    & Unit/Integrate  Quét tĩnh       & Push
                                                        │
                                                        ▼
                                        [ Azure Container Registry ]
                                                        │
                                                        ▼ SSH Pull Image
                                            [ Azure VM: K3s Cluster ]
                                         (identity, catalog, ordering...)
```

### 4.1. Ảo hóa cấp độ Container với Docker

#### 4.1.1. Bản chất và vai trò của Docker
Docker đóng vai trò nền tảng trong việc chuẩn hóa môi trường hoạt động của dự án. Docker loại bỏ hoàn toàn hiện tượng bất tương thích môi trường chạy bằng cách đóng gói mã nguồn, môi trường thực thi (.NET SDK/Nginx runtime), các cấu hình phụ thuộc vào một Docker Image độc lập, đảm bảo ứng dụng chạy nhất quán ở môi trường local của nhà phát triển cũng như trên máy chủ Cloud.

#### 4.1.2. Phân tích Dockerfile cho Frontend (Multi-stage Build)
Dockerfile của frontend được viết theo mẫu tối ưu hóa đa giai đoạn (Multi-stage Build):
- **Stage 1 (Build)**: Sử dụng ảnh `.NET SDK 10.0` để biên dịch mã nguồn C# và tích hợp Node.js 20 để biên dịch Tailwind CSS. Xuất bản mã nguồn Blazor WebAssembly sang các tệp tin tĩnh tối ưu dung lượng thông qua lệnh `dotnet publish -c Release`.
- **Stage 2 (Production)**: Sử dụng ảnh web server siêu nhẹ `nginx:alpine`. Sao chép các tệp tin tĩnh đã xuất bản từ Stage 1 vào thư mục phục vụ của Nginx và cấu hình SPA routing (`try_files $uri $uri/ /index.html;`) giúp trình duyệt xử lý URL Blazor chính xác. Ảnh thành phẩm nhẹ (chỉ khoảng vài chục MB), khởi động pod nhanh và giảm thiểu bề mặt tấn công bảo mật.

#### 4.1.3. Khởi chạy cục bộ với Docker Compose
Sử dụng tệp `docker-compose.yml` để liên kết 6 microservices backend cùng các cơ sở dữ liệu phụ thuộc (SQL Server, Redis, MongoDB). Toàn bộ hệ thống có thể khởi chạy đồng bộ chỉ bằng một câu lệnh `docker-compose up --build`, tạo ra một mạng ảo nội bộ an toàn để các dịch vụ tự động kết nối với nhau.

### 4.2. Vận hành Container với Kubernetes (K3s)

#### 4.2.1. Giới thiệu cụm K3s
K3s là bản phân phối Kubernetes siêu nhẹ do Rancher phát triển, được tối ưu hóa cho máy chủ ảo (Azure VM) cấu hình thấp. Bằng cách lược bỏ các driver đám mây không cần thiết và gộp các tiến trình quản lý cụm, K3s chỉ sử dụng khoảng 512MB RAM nhưng vẫn cung cấp đầy đủ các tính năng tiêu chuẩn của Kubernetes.

#### 4.2.2. Cấu trúc tài nguyên Kubernetes
Chúng tôi định nghĩa hạ tầng hệ thống thông qua các tài nguyên Kubernetes sau:
- **ConfigMaps**: Lưu trữ các biến cấu hình không nhạy cảm (như địa chỉ URL nội bộ của các microservice, ASPNETCORE_ENVIRONMENT).
- **Secrets**: Lưu trữ thông tin nhạy cảm ở dạng mã hóa Base64 (chuỗi kết nối Database, mật khẩu Redis, khóa API Cloudinary, khóa bảo mật JWT).
- **Deployments**: Quản lý số lượng bản sao pod (Replica), cơ chế tự động khởi động lại container bị sập và chiến lược cập nhật phiên bản mới.
- **Services**: Cung cấp địa chỉ IP nội bộ và DNS ổn định cho các pod (ví dụ: `http://catalog-service:8080`), đồng thời thực hiện cân bằng tải nội bộ.
- **Ingress**: Tiếp nhận các yêu cầu HTTPS từ ngoài Internet và thực hiện định tuyến dựa trên đường dẫn (Path-based routing) để phân phối yêu cầu đến các Service tương ứng.

### 4.3. Quy trình Triển khai liên tục (CD) tự động qua SSH
Quy trình CD tự động hóa hoàn toàn việc triển khai ứng dụng từ kho chứa lên máy chủ Azure VM:

#### 4.3.1. Tạo file cấu hình động từ GitHub Secrets
Các thông tin bảo mật kết nối CSDL và API Key được lưu trữ an toàn trong GitHub Actions Secrets. Khi pipeline CD kích hoạt, hệ thống sẽ tự động đọc các Secrets này và ghi ra một tệp tin cấu hình tạm `.env.deploy` ngay trên máy ảo runner.

#### 4.3.2. Thực thi triển khai qua SSH
- **Sao chép hạ tầng**: Dùng `scp-action` truyền tải tệp cấu hình tạm `.env.deploy` và các file k8s cấu hình (`k8s/`) lên máy chủ ảo Azure VM qua cổng SSH.
- **Cập nhật Kubernetes Secrets**: Chạy câu lệnh cập nhật an toàn:
  `kubectl create secret generic app-secrets --from-env-file=.env.deploy --dry-run=client -o yaml | kubectl apply -f -`
- **Áp dụng tài nguyên & Rolling Update**: Chạy lệnh `kubectl apply` để cập nhật cấu hình deployment. Kubernetes sẽ lần lượt tắt pod cũ và bật pod mới chạy mã nguồn mới sau khi pod mới đã vượt qua bước kiểm tra trạng thái sức khỏe (Health checks), đảm bảo tính sẵn sàng 100% (Zero-downtime).
- **Dọn dẹp bảo mật**: Tiêu hủy tệp tin `.env.deploy` tạm thời trên máy chủ Azure để ngăn chặn rủi ro lộ lọt thông tin.

### 4.4. Giám sát hệ thống và Ghi nhật ký tập trung (Observability)

#### 4.4.1. Thu thập metrics với Prometheus và Grafana
- **Prometheus**: Định kỳ kéo các chỉ số hiệu năng (metrics) từ các pod microservice thông qua cổng `/metrics` đã được tích hợp bằng thư viện `Prometheus-net` (bao gồm: tải CPU/RAM, số lượng HTTP request, tỷ lệ lỗi 5xx, thời gian phản hồi API).
- **Grafana**: Đóng vai trò trực quan hóa dữ liệu chỉ số. Hệ thống cấu hình bảng điều khiển trực quan dựa trên file cấu hình [microservices-overview.json](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/monitoring/grafana/dashboards/microservices-overview.json) giúp nhóm vận hành giám sát sức khỏe toàn bộ 6 dịch vụ microservices trên cùng một giao diện đồ họa thời gian thực.

#### 4.4.2. Quản lý log tập trung với Loki & Promtail và cảnh báo qua Alertmanager
- **Loki & Promtail**: Promtail chạy dưới dạng DaemonSet trên các node của Kubernetes, tự động đọc nhật ký hoạt động (log) xuất ra từ container và đẩy về Loki server để đánh chỉ mục. Đội ngũ vận hành có thể tra cứu nhanh chóng log của bất kỳ pod nào trực tiếp trên Grafana bằng ngôn ngữ truy vấn LogQL.
- **Alertmanager**: Prometheus giám sát các chỉ số hiệu năng và so sánh với các ngưỡng cảnh báo định sẵn. Nếu phát hiện sự cố (như microservice bị sập, RAM quá tải > 80% liên tục 5 phút), Alertmanager sẽ lập tức phát thông báo cảnh báo qua kênh Discord/Email để đội ngũ vận hành can thiệp xử lý kịp thời.

---

## KẾT LUẬN

### 5.1. Kết quả đạt được
Dự án đã xây dựng thành công một quy trình phát triển và vận hành phần mềm khép kín, tự động hóa cao áp dụng phương pháp luận DevOps hiện đại cho hệ thống FashionStore. Các thành tựu nổi bật bao gồm:
- Phân rã nghiệp vụ thương mại điện tử thành 6 dịch vụ độc lập hiệu năng cao chạy bằng C# .NET 10 kết hợp với ứng dụng khách Blazor WebAssembly và CSS Tailwind v4.
- Thiết lập quy trình CI tự động hóa việc build, chạy test và quét chất lượng tĩnh bằng SonarCloud, triệt tiêu mã nguồn xấu (bad code) và các lỗ hổng bảo mật nghiêm trọng.
- Triển khai hạ tầng điều phối Container với Kubernetes (K3s) trên máy chủ ảo, quản lý cấu hình thông qua ConfigMaps và Secrets động.
- Triển khai thành công bộ kiểm thử đơn vị tự động (Unit Testing - 83 tests) và bộ kiểm thử tích hợp hoàn chỉnh (Integration Testing - 9 tests) chạy in-memory để đạt độ bao phủ toàn diện cho hệ thống mà không cần cài đặt SQL vật lý.
- Triển khai toàn diện hệ thống giám sát hiệu năng (Prometheus, Grafana) và thu thập log tập trung (Loki, Promtail) cùng hệ thống cảnh báo (Alertmanager) trên Kubernetes, giúp theo dõi vận hành thời gian thực.
- Tự động hóa quá trình deploy bằng CD GitHub Actions thông qua kết nối SSH an toàn, thực hiện chiến lược Rolling Update giúp hệ thống nâng cấp không gián đoạn.

### 5.2. Khó khăn và bài học kinh nghiệm
Trong quá trình triển khai, nhóm đã gặp phải và rút ra được các bài học kinh nghiệm quý báu:
- **Tối ưu hóa tài nguyên**: Việc vận hành hệ thống microservices phức tạp trên máy chủ ảo Azure VM có dung lượng RAM giới hạn đòi hỏi phải kiểm soát nghiêm ngặt tài nguyên CPU/RAM cấp phát (Resource Limits/Requests) cho từng pod.
- **Tách biệt cấu hình bảo mật**: Quá trình triển khai CD yêu cầu sử dụng GitHub Secrets kết hợp Kubernetes Secrets để truyền tải thông tin cấu hình, tuyệt đối không được ghi đè các tham số nhạy cảm lên mã nguồn git.
- **Tái cấu trúc mã nguồn hỗ trợ kiểm thử**: Trích xuất các Dependency thành Interface (như trong ChatService) là bước bắt buộc để có thể mô phỏng và thực hiện kiểm thử tự động, nâng cao tính mô đun hóa của sản phẩm.

### 5.3. Định hướng phát triển tương lai
Dựa trên nền tảng đã xây dựng, nhóm đề xuất các hướng phát triển và cải tiến trong tương lai:
- **Triển khai GitOps với ArgoCD**: Thay thế cơ chế deploy qua SSH script thô sơ bằng công cụ ArgoCD — giải pháp GitOps tiên tiến cho phép theo dõi và đồng bộ trạng thái hệ thống trực tiếp từ mã nguồn Git.
- **Bổ sung bộ kiểm thử tự động Frontend**: Từng bước bổ sung bộ test suite frontend bằng bUnit và tích hợp vào pipeline kiểm thử tự động của GitHub Actions, đạt mục tiêu độ bao phủ kiểm thử tối thiểu 80%.
- **Horizontal Pod Autoscaler (HPA)**: Cấu hình tự động mở rộng số lượng pod dựa trên chỉ số tải CPU/RAM để đảm bảo hệ thống chịu tải tốt trong các đợt cao điểm (như Black Friday, Flash Sale).

---

## TÀI LIỆU THAM KHẢO

[1] Microsoft. (2024). *Blazor WebAssembly documentation*. https://docs.microsoft.com/aspnet/core/blazor/

[2] Docker Inc. (2024). *Docker Documentation — Multi-stage builds*. https://docs.docker.com/build/building/multi-stage/

[3] Kubernetes.io. (2024). *Kubernetes Documentation — Deployments*. https://kubernetes.io/docs/concepts/workloads/controllers/deployment/

[4] GitHub. (2024). *GitHub Actions — Understanding GitHub Actions*. https://docs.github.com/actions/learn-github-actions/understanding-github-actions

[5] SonarSource. (2024). *SonarCloud Documentation*. https://docs.sonarcloud.io/

[6] Microsoft Azure. (2024). *Azure Container Registry documentation*. https://learn.microsoft.com/azure/container-registry/

[7] Rancher Labs. (2024). *K3s — Lightweight Kubernetes*. https://k3s.io/

[8] Redis Ltd. (2024). *Redis Documentation*. https://redis.io/documentation

[9] MongoDB Inc. (2024). *MongoDB Atlas Documentation*. https://www.mongodb.com/docs/atlas/

[10] Cloudinary Ltd. (2024). *Cloudinary Documentation*. https://cloudinary.com/documentation

[11] Newman, S. (2021). *Building Microservices: Designing Fine-Grained Systems* (2nd ed.). O'Reilly Media.

[12] Kim, G., Humble, J., Debois, P., & Willis, J. (2016). *The DevOps Handbook*. IT Revolution Press.

[13] Fowler, M., & Lewis, J. (2014). *Microservices: A definition of this new architectural term*. MartinFowler.com.

[14] Evans, E. (2003). *Domain-Driven Design: Tackling Complexity in the Heart of Software*. Addison-Wesley.
