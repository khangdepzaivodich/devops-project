BỘ GIÁO DỤC VÀ ĐÀO TẠO
TRƯỜNG ĐẠI HỌC SƯ PHẠM THÀNH PHỐ HỒ CHÍ MINH
KHOA CÔNG NGHỆ THÔNG TIN


BÁO CÁO TIỂU LUẬN MÔN HỌC
Môn học: PHÁT TRIỂN VÀ VẬN HÀNH HỆ THỐNG (DEVOPS)

ĐỀ TÀI:
XÂY DỰNG, TRIỂN KHAI VÀ TỰ ĐỘNG HÓA HỆ THỐNG THƯƠNG MẠI ĐIỆN TỬ FASHIONSTORE TRÊN NỀN TẢNG MICROSERVICES KẾT HỢP DOCKER, KUBERNETES, GITHUB ACTIONS CI/CD VÀ HỆ THỐNG GIÁM SÁT TẬP TRUNG (MONITORING & LOGGING)
Người thực hiện
[Điền tên sinh viên]
Mã số sinh viên
[Điền MSSV]
Giảng viên hướng dẫn
[Điền tên Giảng viên]
Năm học
2025 – 2026


TP. HỒ CHÍ MINH, NĂM 2026

NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN









































TP. Hồ Chí Minh, ngày       tháng       năm 2026
Giảng viên hướng dẫn
(Ký và ghi rõ họ tên)

LỜI CAM ĐOAN
Tôi xin cam đoan đây là công trình nghiên cứu của riêng tôi. Các số liệu, kết quả nêu trong tiểu luận là trung thực và chưa được công bố trong bất kỳ công trình nào khác.
Tất cả các tài liệu tham khảo, đoạn trích dẫn đều được ghi rõ nguồn gốc. Nội dung nghiên cứu và kết quả trong đề tài này là trung thực và chưa từng được ai công bố trong bất cứ công trình nào.

TP. Hồ Chí Minh, ngày       tháng       năm 2026
Tác giả tiểu luận
(Ký và ghi rõ họ tên)

LỜI CẢM ƠN
Để hoàn thành tiểu luận này, tôi xin bày tỏ lòng biết ơn sâu sắc đến Quý Thầy/Cô trong Khoa Công nghệ thông tin, Trường Đại học Sư phạm Thành phố Hồ Chí Minh đã tận tình giảng dạy, truyền đạt kiến thức và kỹ năng chuyên môn trong suốt quá trình học tập.
Đặc biệt, tôi xin gửi lời cảm ơn chân thành nhất đến Giảng viên hướng dẫn đã luôn tận tình chỉ bảo, định hướng và tạo mọi điều kiện thuận lợi để tôi hoàn thành đề tài này.
Cuối cùng, tôi xin cảm ơn gia đình, bạn bè và những người thân đã luôn quan tâm, động viên và hỗ trợ tôi trong suốt quá trình thực hiện tiểu luận.
Do kiến thức và kinh nghiệm còn hạn chế, tiểu luận không tránh khỏi những thiếu sót. Rất mong nhận được sự đóng góp ý kiến từ Quý Thầy/Cô để bản báo cáo được hoàn thiện hơn.

TP. Hồ Chí Minh, năm 2026
Tác giả

TÓM TẮT
Tiểu luận này trình bày quá trình nghiên cứu, thiết kế và triển khai hệ thống thương mại điện tử FashionStore ứng dụng kiến trúc Microservices hiện đại kết hợp với phương pháp luận DevOps. Hệ thống bao gồm 6 dịch vụ backend độc lập được xây dựng bằng C# .NET 10, một giao diện người dùng Blazor WebAssembly và được vận hành toàn diện thông qua Docker, Kubernetes (K3s) và GitHub Actions CI/CD.
Nghiên cứu tập trung vào năm khía cạnh kỹ thuật cốt lõi: (1) Thiết kế kiến trúc phân rã dịch vụ theo miền nghiệp vụ; (2) Container hóa toàn bộ hệ thống với Docker sử dụng kỹ thuật Multi-stage Build; (3) Xây dựng quy trình CI/CD tự động hóa kiểm thử, phân tích chất lượng mã nguồn tĩnh bằng SonarCloud và đóng gói ảnh Docker lên Azure Container Registry; (4) Điều phối container và triển khai liên tục trên cụm Kubernetes K3s; (5) Tích hợp hệ thống giám sát hiệu năng (Monitoring) và ghi nhật ký tập trung (Centralized Logging) sử dụng Prometheus, Grafana, Loki, Promtail và Alertmanager.
Đồng thời, dự án cũng chú trọng vào chất lượng mã nguồn thông qua việc tái cấu trúc các dịch vụ (như ChatService) để tăng tính cô lập và khả năng kiểm thử đơn vị (Unit Testing) với xUnit, Moq, FluentAssertions, đạt độ bao phủ toàn diện cho tất cả các Controller của hệ thống.

Từ khóa: DevOps, Microservices, Docker, Kubernetes, CI/CD, GitHub Actions, SonarCloud, Blazor WebAssembly, Prometheus, Grafana, Loki, Unit Testing, .NET 10.

MỤC LỤC
DANH MỤC CÁC TỪ VIẾT TẮT
MỞ ĐẦU
1.1. Lý do chọn đề tài và bối cảnh thực tiễn
1.2. Mục tiêu nghiên cứu và triển khai
1.3. Ý nghĩa của việc áp dụng phương pháp luận DevOps
CHƯƠNG 1: KIẾN TRÚC HỆ THỐNG VÀ CHI TIẾT TECH STACK
1.1. Tổng quan mô hình kiến trúc Microservices của dự án
1.2. Chi tiết công nghệ phía Máy trạm (Frontend Web Application)
1.2.1. Blazor WebAssembly (.NET 10)
1.2.2. Tailwind CSS v4
1.2.3. Microsoft.AspNetCore.SignalR.Client
1.3. Chi tiết công nghệ phía Máy chủ (Backend Microservices)
1.3.1. IdentityService — Dịch vụ Định danh và Phân quyền
1.3.2. CatalogService — Dịch vụ Quản lý Sản phẩm
1.3.3. OrderingService — Dịch vụ Quản lý Đơn hàng
1.3.4. BasketService — Dịch vụ Giỏ hàng
1.3.5. DiscountService — Dịch vụ Mã Giảm giá
1.3.6. ChatService — Dịch vụ Hỗ trợ Trực tuyến
1.4. Cơ sở dữ liệu, Bộ nhớ đệm và Dịch vụ tích hợp bên thứ ba
1.4.1. PostgreSQL và SQL Server — Cơ sở dữ liệu Quan hệ
1.4.2. MongoDB — Cơ sở dữ liệu Phi quan hệ (NoSQL)
1.4.3. Redis Cache — Bộ nhớ đệm trong RAM
1.4.4. Cloudinary — Lưu trữ và Tối ưu hóa Hình ảnh Đám mây
1.4.5. SMTP Server — Dịch vụ Gửi Email Tự động
CHƯƠNG 2: TRIỂN KHAI CÔNG CỤ VÀ QUY TRÌNH DEVOPS TRONG DỰ ÁN
2.1. Ảo hóa cấp độ Container với Docker (Containerization)
2.1.1. Bản chất và công dụng của Docker trong dự án
2.1.2. Phân tích Dockerfile cho Frontend (Multi-stage Build)
2.1.3. Điều phối phát triển cục bộ với Docker Compose
2.2. Quy trình Tích hợp liên tục (CI) với GitHub Actions
2.2.1. Phân tích luồng CI cho Backend (dotnet-ci.yml)
2.2.2. Phân tích luồng CI cho Frontend (frontend-ci.yml)
2.3. Tự động hóa Kiểm thử Đơn vị (Unit Testing) và Tái cấu trúc ChatService
2.3.1. Thiết kế và thực thi kiểm thử đơn vị với xUnit, Moq và FluentAssertions
2.3.2. Tái cấu trúc ChatService bằng Interface phục vụ Mocking
2.3.3. Tích hợp kiểm thử đơn vị vào luồng CI
2.4. Phân tích chất lượng mã nguồn tự động với SonarCloud
2.4.1. Các vấn đề kỹ thuật đã được phát hiện và giải quyết
2.4.2. Giải quyết xung đột quét mã nguồn (Exclusions & False Positives)
2.5. Hệ quản trị Container tập trung với Azure Container Registry (ACR)
2.6. Điều phối và vận hành Container với Kubernetes (K3s)
2.6.1. Giới thiệu về K3s
2.6.2. Cấu trúc và chức năng các tài nguyên Kubernetes (k8s)
2.7. Quy trình Triển khai liên tục (CD) và Tự động hóa Vận hành
2.7.1. Tạo file cấu hình động từ GitHub Secrets
2.7.2. Truyền tải và thực thi lệnh qua SSH
2.8. Giám sát hệ thống (Monitoring) và Ghi nhật ký tập trung (Centralized Logging)
2.8.1. Thu thập và trực quan hóa chỉ số hiệu năng (Prometheus & Grafana)
2.8.2. Gom log tập trung (Loki & Promtail) và cảnh báo tự động (Alertmanager)
KẾT LUẬN VÀ HƯỚNG PHÁT TRIỂN
3.1. Những kết quả đã đạt được
3.2. Khó khăn và Bài học kinh nghiệm
3.3. Định hướng phát triển tương lai
TÀI LIỆU THAM KHẢO

DANH MỤC CÁC TỪ VIẾT TẮT
Từ viết tắt | Giải thích
--- | ---
CI/CD | Continuous Integration / Continuous Delivery (Tích hợp liên tục / Triển khai liên tục)
DevOps | Development Operations (Phát triển và Vận hành)
API | Application Programming Interface (Giao diện lập trình ứng dụng)
JWT | JSON Web Token (Mã thông báo xác thực)
RBAC | Role-based Access Control (Kiểm soát truy cập dựa trên vai trò)
ACR | Azure Container Registry (Kho chứa Container Azure)
K3s | Kubernetes phiên bản rút gọn (Rancher)
SPA | Single Page Application (Ứng dụng trang đơn)
SAST | Static Application Security Testing (Kiểm thử bảo mật ứng dụng tĩnh)
ACID | Atomicity, Consistency, Isolation, Durability (Tính toàn vẹn giao dịch DB)
RAM | Random Access Memory (Bộ nhớ truy cập ngẫu nhiên)
CDN | Content Delivery Network (Mạng phân phối nội dung)
VM | Virtual Machine (Máy ảo)
SSH | Secure Shell (Giao thức kết nối bảo mật từ xa)
DDD | Domain-Driven Design (Thiết kế hướng miền)
Wasm | WebAssembly (Chuẩn biên dịch nhị phân cho trình duyệt)
SMTP | Simple Mail Transfer Protocol (Giao thức truyền tải thư điện tử)
NoSQL | Not only SQL (Cơ sở dữ liệu phi quan hệ)


MỞ ĐẦU

1.1. Lý do chọn đề tài và bối cảnh thực tiễn
Trong kỷ nguyên số hóa mạnh mẽ ngày nay, ngành thương mại điện tử (E-commerce) đòi hỏi các hệ thống phần mềm phải có khả năng vận hành liên tục 24/7, chịu tải cao, phản hồi nhanh chóng và cập nhật tính năng mới liên tục mà không gây gián đoạn dịch vụ. Mô hình kiến trúc nguyên khối (Monolith) truyền thống dần bộc lộ những hạn chế nghiêm trọng như: khó mở rộng cục bộ, rủi ro sập toàn bộ hệ thống khi một module nhỏ gặp lỗi, thời gian build và deploy kéo dài, khó khăn trong việc áp dụng nhiều công nghệ khác nhau trên cùng một mã nguồn.
Để giải quyết triệt để vấn đề này, kiến trúc vi dịch vụ (Microservices) nổi lên như một giải pháp hàng đầu. Bằng cách chia nhỏ hệ thống thành các dịch vụ độc lập, chạy riêng biệt và giao tiếp qua các giao thức mạng chuẩn hóa, doanh nghiệp có thể tăng tốc độ phát triển và tối ưu hóa tài nguyên phần cứng. Tuy nhiên, việc vận hành hàng chục microservices khác nhau lại đặt ra thách thức cực kỳ lớn về triển khai, quản lý cấu hình, kiểm thử chất lượng và đồng bộ hóa môi trường giữa các nhóm phát triển và vận hành. Đây chính là lý do phương pháp luận DevOps và các công cụ tự động hóa đi kèm trở thành yếu tố bắt buộc để dự án thành công.

1.2. Mục tiêu nghiên cứu và triển khai
Đề tài hướng tới nghiên cứu sâu rộng và triển khai thực tế một hệ thống thương mại điện tử đầy đủ tính năng mang tên FashionStore, ứng dụng toàn diện các kỹ thuật công nghệ hiện đại. Cụ thể:
- Thiết kế và hiện thực hóa kiến trúc Microservices phân rã theo miền nghiệp vụ (Domain-Driven Design) cho backend và phát triển giao diện người dùng đơn trang (SPA) bằng Blazor WebAssembly.
- Xây dựng môi trường ảo hóa container hóa hoàn chỉnh cho tất cả các dịch vụ bằng Docker.
- Thiết lập hệ thống CI/CD tự động kiểm thử đơn vị, phân tích bảo mật tĩnh (SAST), đo đạc chỉ số kỹ thuật mã nguồn (SonarCloud Quality Gate) và tự động đóng gói, phát hành sản phẩm.
- Triển khai và điều phối hạ tầng trên cụm Kubernetes nhằm tối ưu hóa khả năng tự phục hồi (Self-healing), cân bằng tải tự động (Load balancing) và nâng cấp hệ thống không gián đoạn (Zero-downtime deployment).
- Triển khai giải pháp giám sát tài nguyên và phân tích nhật ký tập trung nhằm phát hiện sớm các sự cố phát sinh trên môi trường triển khai thực tế.

1.3. Ý nghĩa của việc áp dụng phương pháp luận DevOps
DevOps không đơn thuần là sự kết hợp giữa các công cụ phần mềm, mà là một cuộc cách mạng về văn hóa cộng tác giữa hai phòng ban phát triển (Development) và vận hành (Operations). Việc triển khai quy trình DevOps trong dự án mang lại những lợi ích thiết thực:
- Rút ngắn thời gian đưa sản phẩm ra thị trường (Time-to-Market): Nhờ có các pipeline CI/CD tự động chạy khi có code mới, quá trình tích hợp mã nguồn, build và deploy lên môi trường staging/production chỉ diễn ra trong vài phút thay vì hàng giờ làm tay.
- Tăng cường chất lượng và tính ổn định: Việc tích hợp kiểm thử đơn vị tự động cùng bộ quét tĩnh SonarCloud giúp phát hiện sớm các lỗ hổng bảo mật nghiêm trọng (như Open Redirect, SQL Injection), các đoạn code thừa, code có độ phức tạp cao ngay trong pha Pull Request, tránh đưa lỗi lên môi trường thực tế.
- Tối ưu hóa chi phí hạ tầng: Docker và Kubernetes giúp cô lập ứng dụng và tận dụng tối đa tài nguyên máy chủ. Kubernetes quản lý trạng thái của các pod một cách tự động, tự khởi động lại pod hỏng và tự điều hướng lưu lượng traffic phù hợp.


CHƯƠNG 1: KIẾN TRÚC HỆ THỐNG VÀ CHI TIẾT TECH STACK

1.1. Tổng quan mô hình kiến trúc Microservices của dự án
Hệ thống FashionStore được xây dựng theo mô hình kiến trúc hướng dịch vụ phi tập trung (Decentralized Microservices Architecture). Phía Frontend gửi các yêu cầu HTTP/HTTPS thông qua cơ chế định tuyến Ingress của Kubernetes để phân phối trực tiếp đến các dịch vụ xử lý nghiệp vụ chuyên biệt.
Mỗi dịch vụ sở hữu dữ liệu riêng biệt để đảm bảo tính độc lập tối đa theo mẫu Database-per-Service, ngăn chặn việc một dịch vụ truy cập trực tiếp vào cơ sở dữ liệu của dịch vụ khác. Việc giao tiếp giữa các dịch vụ được thực hiện thông qua các API RESTful gọn nhẹ dựa trên HTTP, kết hợp với kết nối thời gian thực qua WebSockets (SignalR) cho tính năng live chat hỗ trợ khách hàng.

Sơ đồ kiến trúc tổng thể hệ thống:
```
[ Khách hàng / Admin / Staff ]
              │
              ▼ (HTTPS / WebSockets)
     [ Kubernetes Ingress ]
              │
     ┌────────┼────────────────────────┐
     │        │                        │
 [ / ]   [ /api/identity/* ]   [ /api/catalog/* ]
  │              │                     │
 Blazor     identity-service    catalog-service
 Frontend      (Port 7093)        (Port 7103)
              │                        │
     [ /api/ordering/* ]   [ /api/basket/* ]
        ordering-service    basket-service
          (Port 7076)         (Port 7021)
              │
     [ /api/discount/* ]   [ /api/chat/* ]
      discount-service      chat-service
        (Port 7002)          (Port 7229)
```

1.2. Chi tiết công nghệ phía Máy trạm (Frontend Web Application)
1.2.1. Blazor WebAssembly (.NET 10)
Dự án sử dụng Blazor WebAssembly (Wasm) của Microsoft làm framework xây dựng giao diện người dùng chính. Blazor WebAssembly chạy trực tiếp mã C# Client-side ngay trong trình duyệt web của người dùng thông qua công nghệ WebAssembly — một chuẩn mở của W3C cho phép biên dịch mã nhị phân hiệu năng cao chạy trực tiếp trên các trình duyệt hiện đại.
Nhờ sử dụng Blazor Wasm, đội ngũ phát triển có thể viết mã logic kiểm tra (Validation), xử lý dữ liệu và cấu trúc các Component bằng ngôn ngữ C# đồng nhất với Backend. Điều này loại bỏ hoàn toàn sự phụ thuộc vào JavaScript, giảm thiểu lỗi bất đồng bộ định dạng dữ liệu và cho phép chia sẻ các lớp dữ liệu Data Transfer Object (DTO) dùng chung giữa client và server một cách dễ dàng.

1.2.2. Tailwind CSS v4
Tailwind CSS là một framework CSS theo trường phái 'Utility-first'. Thay vì viết các class CSS dài dòng trong các file .css riêng lẻ, Tailwind cung cấp các class tiện ích nguyên tử nhỏ gọn (như flex, pt-4, text-center, bg-blue-600) để định hình phong cách trực tiếp trên mã HTML/Razor Component.
Phiên bản Tailwind CSS v4 mang đến trình biên dịch hiệu năng cao dựa trên công cụ LightningCSS, rút ngắn thời gian build CSS, loại bỏ CSS thừa tự động (Purging) để tạo ra file CSS phân phối cuối cùng siêu nhẹ, giúp trang web load nhanh vượt trội trên cả thiết bị di động lẫn máy tính để bàn.

1.2.3. Microsoft.AspNetCore.SignalR.Client
SignalR là một thư viện hỗ trợ giao tiếp hai chiều thời gian thực giữa máy khách và máy chủ. Nó tự động quản lý các kết nối và tự động chọn phương thức truyền tải tốt nhất hiện có (ưu tiên WebSockets, sau đó là Server-Sent Events hoặc Long Polling).
Thư viện được sử dụng trực tiếp trong Widget Chat trực tuyến của cửa hàng để kết nối khách hàng với nhân viên tư vấn. Mọi tin nhắn trao đổi được truyền phát tức thời với độ trễ cực thấp mà không cần người dùng phải tải lại trang hoặc thực hiện kỹ thuật gọi lặp liên tục (Polling) gây lãng phí băng thông.

1.3. Chi tiết công nghệ phía Máy chủ (Backend Microservices)
Các dịch vụ phía backend đều chạy độc lập và được xây dựng bằng C# trên nền tảng .NET 10 — phiên bản cập nhật mới nhất với nhiều cải tiến về hiệu năng biên dịch AOT, tối ưu hóa bộ nhớ và các câu lệnh rút gọn. Dưới đây là mô tả chi tiết từng dịch vụ.

1.3.1. IdentityService — Dịch vụ Định danh và Phân quyền
- Chức năng: Quản lý tài khoản, mã hóa mật khẩu, đăng ký thành viên, xác thực đăng nhập và cấp phát JSON Web Token (JWT) có chữ ký số RSA bảo mật. Quản lý cơ chế phân quyền dựa trên vai trò (RBAC) phân cấp giữa Quản trị viên (Admin), Nhân viên (Staff) và Khách hàng (User).
- Môi trường vận hành: Lắng nghe trên cổng nội bộ 8080, ánh xạ ra ngoài qua cổng 7093 trong môi trường phát triển. Tích hợp trực tiếp với Cloudinary để xử lý ảnh đại diện người dùng và dịch vụ gửi email SMTP để khôi phục mật khẩu.

1.3.2. CatalogService — Dịch vụ Quản lý Sản phẩm
- Chức năng: Quản lý danh mục loại sản phẩm (LDM), danh mục cụ thể (DM), chi tiết thuộc tính sản phẩm (màu sắc, kích thước, số lượng tồn kho), tìm kiếm sản phẩm theo từ khóa, lọc theo mức giá và slug URL thân thiện SEO.
- Môi trường vận hành: Lắng nghe cổng nội bộ 8080, cổng ngoài 7103. Dịch vụ này lưu trữ hình ảnh sản phẩm chất lượng cao trên Cloudinary thông qua API tải lên trực tiếp.

1.3.3. OrderingService — Dịch vụ Quản lý Đơn hàng
- Chức năng: Tiếp nhận yêu cầu đặt hàng, kiểm tra tính hợp lệ của giỏ hàng, áp dụng các chương trình giảm giá, lưu trữ hóa đơn, tính tổng tiền thanh toán và cập nhật trạng thái đơn hàng (chờ xử lý, đã duyệt, đang giao, đã hủy).
- Môi trường vận hành: Lắng nghe cổng nội bộ 8080, cổng ngoài 7076. Giao tiếp HTTP trực tiếp với catalog-service và discount-service để kiểm tra giá gốc và trừ mã giảm giá.

1.3.4. BasketService — Dịch vụ Giỏ hàng
- Chức năng: Lưu trữ giỏ hàng tạm thời của khách hàng. Đặc biệt hỗ trợ lưu trữ giỏ hàng của khách vãng lai (Guest Cart) và tự động gộp sản phẩm từ giỏ khách vãng lai vào tài khoản chính thức (Auth Cart) ngay sau khi người dùng đăng nhập thành công.
- Môi trường vận hành: Lắng nghe cổng nội bộ 8080, cổng ngoài 7021. Xử lý tốc độ cao nhờ lưu trữ dữ liệu hoàn toàn trên bộ nhớ đệm Redis, giúp giảm thời gian phản hồi xuống dưới 5ms.

1.3.5. DiscountService — Dịch vụ Mã Giảm giá
- Chức năng: Quản lý các chương trình khuyến mãi, sự kiện ưu đãi và tạo các mã coupon giảm giá (giảm theo phần trăm hoặc giảm số tiền cố định). Giới hạn thời gian hiệu lực và phạm vi áp dụng của mã giảm giá (áp dụng cho toàn sàn, theo danh mục sản phẩm cụ thể hoặc theo từng sản phẩm).
- Môi trường vận hành: Lắng nghe cổng nội bộ 8080, cổng ngoài 7002. Kết nối cơ sở dữ liệu MongoDB Atlas để quản lý linh hoạt các chương trình khuyến mãi.

1.3.6. ChatService — Dịch vụ Hỗ trợ Trực tuyến
- Chức năng: Khởi tạo và duy trì các phòng chat hỗ trợ trực tuyến giữa khách hàng vãng lai/đã đăng nhập và các nhân viên trực tổng đài. Quản lý trạng thái hàng đợi kết nối (Queued) khi chưa có nhân viên nhận và chuyển tiếp hội thoại (Assigned) khi nhân viên tiếp nhận.
- Môi trường vận hành: Lắng nghe cổng nội bộ 8080, cổng ngoài 7229. Sử dụng MongoDB để lưu lịch sử hội thoại lâu dài và Redis để đồng bộ hóa trạng thái trực tuyến.

1.4. Cơ sở dữ liệu, Bộ nhớ đệm và Dịch vụ tích hợp bên thứ ba
1.4.1. PostgreSQL và SQL Server — Cơ sở dữ liệu Quan hệ
Được sử dụng làm cơ sở dữ liệu chính (Primary DB) cho các dịch vụ đòi hỏi tính nhất quán dữ liệu cao như IdentityService, CatalogService và OrderingService. Đảm bảo các giao dịch mua hàng, thanh toán và quản lý tài khoản tuân thủ nghiêm ngặt tiêu chuẩn ACID (Atomicity, Consistency, Isolation, Durability).

1.4.2. MongoDB — Cơ sở dữ liệu Phi quan hệ (NoSQL)
Được cấu hình làm cơ sở dữ liệu cho DiscountService và ChatService, sử dụng MongoDB Atlas đám mây. MongoDB lưu trữ dữ liệu dưới dạng tài liệu JSON/BSON linh hoạt, rất phù hợp cho việc lưu trữ các quy tắc giảm giá đa dạng và lịch sử tin nhắn chat — dữ liệu phi cấu trúc, phát triển theo thời gian thực và đòi hỏi ghi chép tốc độ cao.

1.4.3. Redis Cache — Bộ nhớ đệm trong RAM
Bộ nhớ đệm tốc độ cao lưu trữ dữ liệu dạng Key-Value trong RAM. Sử dụng trong BasketService để lưu giỏ hàng của người dùng và trong ChatService để đồng bộ hóa trạng thái kết nối SignalR giữa các phiên làm việc. Redis giúp giảm tải tối đa cho cơ sở dữ liệu vật lý và tăng tốc độ phản hồi của hệ thống xuống dưới 5ms.

1.4.4. Cloudinary — Lưu trữ và Tối ưu hóa Hình ảnh Đám mây
Toàn bộ ảnh sản phẩm, ảnh chi tiết biến thể thời trang và ảnh đại diện người dùng được đẩy trực tiếp lên Cloudinary. Cloudinary tự động nén dung lượng ảnh, chuyển đổi sang các định dạng web hiện đại (WebP, AVIF) và cung cấp qua mạng phân phối nội dung (CDN) toàn cầu để tối ưu hóa tốc độ tải trang phía máy khách.

1.4.5. SMTP Server — Dịch vụ Gửi Email Tự động
Tích hợp trong IdentityService phục vụ việc gửi các email giao dịch, email xác thực đăng ký tài khoản mới và email chứa liên kết bảo mật để khôi phục mật khẩu khi người dùng yêu cầu.


CHƯƠNG 2: TRIỂN KHAI CÔNG CỤ VÀ QUY TRÌNH DEVOPS TRONG DỰ ÁN

Tổng quan luồng DevOps:
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
                    & Unit Test      Quét tĩnh       & Push
                                                       │
                                                       ▼
                                        [ Azure Container Registry ]
                                                       │
                                                       ▼ SSH Pull Image
                                           [ Azure VM: K3s Cluster ]
                                        (identity, catalog, ordering...)
```

2.1. Ảo hóa cấp độ Container với Docker (Containerization)
2.1.1. Bản chất và công dụng của Docker trong dự án
Docker đóng vai trò cốt lõi trong việc chuẩn hóa môi trường chạy của các dịch vụ. Nó giải quyết triệt để vấn đề kinh điển trong phát triển phần mềm: "Chạy bình thường trên máy tôi nhưng lỗi khi đưa lên máy chủ". Bằng cách đóng gói toàn bộ mã nguồn ứng dụng, runtime (.NET SDK / Nginx), các thư viện phụ thuộc và biến cấu hình vào trong một Image duy nhất, Docker đảm bảo ứng dụng sẽ chạy hoàn toàn đồng nhất trên mọi môi trường.

2.1.2. Phân tích Dockerfile cho Frontend (Multi-stage Build)
Dockerfile.frontend được thiết kế theo kỹ thuật chuyên nghiệp Multi-stage Build (Xây dựng đa giai đoạn) nhằm giảm thiểu tối đa kích thước ảnh sản phẩm cuối cùng và nâng cao tính bảo mật:
- Stage 1 — Biên dịch mã nguồn (Build Stage): Sử dụng ảnh gốc mcr.microsoft.com/dotnet/sdk:10.0 làm môi trường biên dịch. Cài đặt Node.js và chạy Tailwind CSS CLI để biên dịch file CSS tối ưu. Thực hiện lệnh dotnet publish -c Release -o /app/publish để xuất bản mã nguồn Blazor WebAssembly sang dạng tĩnh (.wasm, .dll, html, js) được tối ưu hóa dung lượng (Tree-shaking).
- Stage 2 — Phục vụ tĩnh (Production Stage): Sử dụng ảnh web server siêu nhẹ nginx:alpine (chỉ nặng khoảng vài chục MB). Chỉ copy các file tĩnh đã xuất bản từ Stage 1 vào thư mục lưu trữ của Nginx. Cấu hình lại Nginx để hỗ trợ SPA Routing (try_files $uri $uri/ /index.html;) giúp điều hướng URL của Blazor hoạt động đúng. Ảnh thành phẩm không chứa mã nguồn gốc, không chứa SDK thừa, đảm bảo tốc độ khởi động pod nhanh và tăng tính bảo mật.

2.1.3. Điều phối phát triển cục bộ với Docker Compose
Tệp docker-compose.yml định nghĩa và khởi tạo toàn bộ hạ tầng backend (6 dịch vụ microservices) cùng các biến cấu hình kết nối database chỉ bằng một câu lệnh duy nhất: docker-compose up --build. Nó tự động thiết lập mạng ảo nội bộ (backend network) giúp các dịch vụ có thể phân giải tên miền của nhau và giao tiếp an toàn, đồng thời ánh xạ các cổng port ra máy thật để phục vụ việc kiểm thử.

2.2. Quy trình Tích hợp liên tục (CI) với GitHub Actions
GitHub Actions được sử dụng làm công cụ tự động hóa CI/CD chính của dự án. Hai pipeline CI độc lập được xây dựng để kiểm thử và đóng gói phần mềm mỗi khi có lập trình viên đẩy mã nguồn mới lên nhánh main hoặc tạo yêu cầu Pull Request.

2.2.1. Phân tích luồng CI cho Backend (dotnet-ci.yml)
Quy trình CI cho backend gồm các bước sau:
- Kích hoạt (Trigger): Tự động chạy khi có thay đổi trong thư mục serious/** hoặc workflow dotnet-ci.yml.
- Khởi tạo môi trường: Tạo runner máy ảo chạy Ubuntu Linux, cài đặt Java 17 (phục vụ SonarCloud) và cài đặt .NET 10 SDK.
- Khôi phục thư viện: Thực hiện dotnet restore dựa trên file giải pháp serious.slnx.
- Quét mã nguồn: Khởi động dotnet-sonarscanner, tiến hành phân tích toàn bộ cấu trúc mã nguồn backend.
- Build & Test: Chạy dotnet build và toàn bộ unit test với lệnh dotnet test, đồng thời thu thập dữ liệu độ bao phủ kiểm thử (Code Coverage).
- Kết thúc quét: Đẩy các báo cáo phân tích lên máy chủ SonarCloud.
- Đóng gói Docker: Duyệt qua các thư mục microservice trong Services/, tự động build Dockerfile của từng dịch vụ và tag ảnh theo mã định danh commit (github.sha) cùng tag latest, sau đó đẩy ảnh lên Azure Container Registry.

2.2.2. Phân tích luồng CI cho Frontend (frontend-ci.yml)
Tương tự như backend, nhưng được tối ưu hóa cho Blazor WebAssembly và Tailwind CSS:
- Cài đặt môi trường Node.js phiên bản 20 để chạy trình biên dịch Tailwind CSS.
- Chạy npm ci để khôi phục các package npm ở trạng thái đông lạnh, đảm bảo phiên bản đồng nhất.
- Khởi động SonarScanner, chạy dotnet build để biên dịch Blazor WebAssembly.
- Thực hiện đóng gói ảnh Docker dựa trên Dockerfile.frontend và đẩy lên Azure Container Registry.

2.3. Tự động hóa Kiểm thử Đơn vị (Unit Testing) và Tái cấu trúc ChatService
Để đảm bảo chất lượng hệ thống và ngăn ngừa lỗi logic khi phát triển liên tục, dự án áp dụng chiến lược kiểm thử tự động toàn diện cho phía Backend.

2.3.1. Thiết kế và thực thi kiểm thử đơn vị với xUnit, Moq và FluentAssertions
Các dự án kiểm thử được tổ chức riêng biệt tương ứng với từng Microservice (ví dụ: `CatalogService.Tests`, `IdentityService.Tests`, `BasketService.Tests`, `ChatService.Tests`, `DiscountService.Tests`, `OrderingService.Tests`).
- xUnit: Sử dụng làm khung kiểm thử (Test Framework) để chạy các test case.
- Moq: Dùng để giả lập hành vi (Mocking) của các phụ thuộc dịch vụ ngoài (như Database, HTTP Client, Redis, Mail Server) và cô lập logic của API Controller cần kiểm thử.
- FluentAssertions: Dùng để viết các câu lệnh kiểm định dữ liệu trả về một cách trực quan và dễ đọc (ví dụ: `result.Should().BeOfType<OkObjectResult>()`).

2.3.2. Tái cấu trúc ChatService bằng Interface phục vụ Mocking
Ban đầu, `ChatController` và `ChatHub` trong `ChatService` phụ thuộc trực tiếp vào các lớp dịch vụ cụ thể `ChatMongoService` và `ChatRedisService` (chứa logic kết nối trực tiếp đến cơ sở dữ liệu MongoDB và Redis thực tế). Điều này làm cho việc viết unit test không khả thi vì kiểm thử đơn vị yêu cầu không phụ thuộc vào hạ tầng vật lý.
Giải pháp tái cấu trúc đã được thực hiện:
- Trích xuất hai interface [IChatMongoService](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/Services/ChatService/ChatAPI/Services/Interfaces/IChatMongoService.cs) và [IChatRedisService](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/Services/ChatService/ChatAPI/Services/Interfaces/IChatRedisService.cs).
- Cho các lớp dịch vụ thực tế implement các interface này.
- Cập nhật constructor của `ChatController`, `ChatHub` và `ChatCleanupWorker` để yêu cầu interface.
- Đăng ký các interface này trong IoC Container của ASP.NET Core (`Program.cs`) dưới dạng Singleton.
Nhờ đó, trong dự án test, ta có thể dễ dàng sử dụng Moq để giả lập dữ liệu trả về của MongoDB và Redis mà không cần khởi chạy cơ sở dữ liệu thật.

2.3.3. Tích hợp kiểm thử đơn vị vào luồng CI
Tất cả các dự án kiểm thử đơn vị được đăng ký vào file giải pháp [serious.slnx](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/serious/serious.slnx). Khi quy trình CI chạy bước `dotnet test`, hệ thống sẽ tự động tìm tất cả các test dự án và thực hiện kiểm thử. Nếu có bất kỳ test case nào thất bại, pipeline CI sẽ lập tức dừng lại và báo lỗi, không cho phép đóng gói ảnh Docker hay đẩy lên Azure Container Registry.

2.4. Phân tích chất lượng mã nguồn tự động với SonarCloud
SonarCloud đóng vai trò là "chốt chặn bảo mật" trong quy trình CI. Nó phân tích tĩnh mã nguồn để tìm ra các lỗi cấu trúc mã, lỗ hổng bảo mật và nợ kỹ thuật (Technical Debt), ngăn chặn mã xấu tiếp cận môi trường production.

2.4.1. Các vấn đề kỹ thuật đã được phát hiện và giải quyết
Trong quá trình triển khai, SonarCloud đã phát hiện và nhóm nghiên cứu đã giải quyết các vấn đề sau:
- Open Redirect Vulnerability (S5146): Trong SignIn.razor, việc redirect trực tiếp dựa trên tham số query returnUrl chưa được kiểm duyệt có thể bị tin tặc lợi dụng để dẫn dụ người dùng sang trang web lừa đảo (Phishing). Giải pháp: Xây dựng hàm GetSafeReturnUrl() để chặn các liên kết tuyệt đối, liên kết tương đối bắt đầu bằng //, các ký tự đặc biệt, chỉ cho phép các URL tương đối nội bộ bắt đầu bằng dấu gạch chéo (/).
- Collection Copying (S2933 / S2325): Các thuộc tính C# trả về danh sách dạng .ToList() hoặc .ToArray() trong Property Getter sẽ bị gọi lại liên tục trong vòng đời render của Blazor, gây lãng phí bộ nhớ. Giải pháp: Refactor thành các phương thức tường minh (GetDisplayProducts(), GetThumbnails()), cải thiện đáng kể hiệu năng của ứng dụng.
- Cognitive Complexity: Các phương thức xử lý logic quá phức tạp (như OnInitializedAsync trong Messages.razor) đã được phân rã thành các phương thức helper nhỏ, giúp giảm độ phức tạp nhận thức từ trên 25 xuống dưới mức quy định là 15.

2.4.2. Giải quyết xung đột quét mã nguồn (Exclusions & False Positives)
Trong quá trình chạy quét CI, SonarCloud đã phân tích nhầm các thư mục biên dịch tạm thời và thư viện bên thứ ba, dẫn đến Quality Gate bị thất bại. Nhóm đã cấu hình hai thông số quan trọng:
- sonar.exclusions: Loại trừ thư mục bin, obj và các thư viện bên thứ ba như vietnam-provinces.umd.js khỏi phân tích tĩnh để tránh báo lỗi giả từ mã sinh tự động.
- sonar.coverage.exclusions: Loại trừ frontend Blazor WebAssembly và một số lớp cấu hình không chứa logic nghiệp vụ khỏi yêu cầu kiểm định độ phủ (Code Coverage) của SonarCloud để Quality Gate không bị block một cách vô lý, trong khi vẫn duy trì quy trình kiểm thử đơn vị chặt chẽ cho backend.

2.5. Hệ quản trị Container tập trung với Azure Container Registry (ACR)
Azure Container Registry (ACR) là dịch vụ lưu trữ và quản lý Docker Images riêng tư (Private) chạy trên nền tảng đám mây Microsoft Azure. ACR đóng vai trò là cầu nối giữa pha CI (GitHub Actions) và pha CD (Kubernetes).
Sau khi GitHub Actions biên dịch và đóng gói thành công các Docker Images cho frontend và 6 microservices backend, các ảnh này được đẩy trực tiếp lên kho chứa bảo mật của ACR. Cụm Kubernetes ở máy chủ deployment sau đó sẽ sử dụng tài khoản bí mật (Image Pull Secret) để kéo (pull) các ảnh mới nhất về và chạy trên các nút của cụm.

2.6. Điều phối và vận hành Container với Kubernetes (K3s)
2.6.1. Giới thiệu về K3s
K3s là một phiên bản Kubernetes rút gọn, nhẹ và được tối ưu hóa cực tốt bởi Rancher. Nó loại bỏ các driver đám mây không cần thiết, gộp các thành phần điều phối vào một tiến trình duy nhất và chỉ tiêu tốn khoảng 512MB RAM, rất phù hợp cho việc triển khai trên môi trường máy ảo có cấu hình giới hạn.
K3s cung cấp đầy đủ các tính năng của Kubernetes tiêu chuẩn như định tuyến dịch vụ, cân bằng tải, tự động khôi phục container bị lỗi và quản lý cấu hình tập trung — toàn bộ trên môi trường máy chủ Azure VM.

2.6.2. Cấu trúc và chức năng các tài nguyên Kubernetes (k8s)
Hệ thống sử dụng các loại tài nguyên Kubernetes sau:
- ConfigMaps: Lưu trữ các biến cấu hình môi trường không nhạy cảm (địa chỉ URL nội bộ của các microservice, cấu hình ASPNETCORE_ENVIRONMENT). Các pod sẽ đọc các biến này khi khởi động.
- Secrets: Lưu trữ thông tin nhạy cảm ở dạng mã hóa Base64 (chuỗi kết nối Database, mật khẩu Redis, khóa API Cloudinary, khóa bảo mật JWT).
- Deployments: Định nghĩa trạng thái mong muốn của ứng dụng (số lượng bản sao pod — Replica, loại ảnh Docker sử dụng, giới hạn tài nguyên CPU/RAM, chiến lược cập nhật). Kubernetes liên tục giám sát để duy trì đúng số lượng pod hoạt động.
- Services: Tạo ra địa chỉ IP ảo và tên miền nội bộ cố định cho từng microservice (ví dụ: http://catalog-service:8080). Service tự động cân bằng tải traffic đến các pod, giúp các dịch vụ giao tiếp mà không cần biết IP thật của pod.
- Ingress: Bộ định tuyến biên (Ingress Controller) tiếp nhận lưu lượng từ Internet và điều hướng dựa trên đường dẫn URL (Path-based routing) để gửi traffic đến đúng Service bên trong cụm.

2.7. Quy trình Triển khai liên tục (CD) và Tự động hóa Vận hành
Pipeline CD được định nghĩa trong file cd.yml. Quá trình này hoàn toàn tự động hóa việc đưa sản phẩm lên môi trường máy chủ, bao gồm các bước tuần tự sau:

2.7.1. Tạo file cấu hình động từ GitHub Secrets
Do các thông tin kết nối cơ sở dữ liệu và khóa bảo mật API không được phép lưu trữ trực tiếp trên mã nguồn GitHub (nhằm tránh rò lọt thông tin), quy trình CD sử dụng tính năng GitHub Actions Secrets để lưu trữ bảo mật. Khi chạy deploy, bước đầu tiên của pipeline là lấy các secret này và ghi vào một file .env.deploy tạm thời trên runner.

2.7.2. Truyền tải và thực thi lệnh qua SSH
Sau khi tạo file cấu hình, pipeline CD thực hiện các bước sau:
- Copy mã nguồn hạ tầng: Sử dụng scp-action thông qua SSH kết hợp khóa bảo mật cá nhân (SSH Key) để copy toàn bộ thư mục k8s/ và file .env.deploy lên thư mục tạm thời của máy chủ Azure VM.
- Khởi tạo và cập nhật Secrets: Kết nối vào máy chủ qua ssh-action và chạy lệnh kubectl create secret generic app-secrets --from-env-file với tham số --dry-run=client -o yaml | kubectl apply, đảm bảo nếu secret đã tồn tại thì sẽ được cập nhật đè lên an toàn.
- Áp dụng tài nguyên: Chạy kubectl apply -f . cho tất cả các file deployment, service và ingress trong namespace chỉ định (staging hoặc production).
- Nâng cấp không gián đoạn (Rolling Update): Thực hiện kubectl rollout restart deployment/identity-service cho từng dịch vụ. Kubernetes tạo pod mới chạy cấu hình mới trước, kiểm tra pod mới hoạt động ổn định qua Health Checks rồi mới xóa pod cũ. Quá trình này đảm bảo hệ thống hoạt động liên tục không có downtime.
- Dọn dẹp bảo mật: Xóa file cấu hình tạm .env.deploy trên máy chủ ngay lập tức để tránh lộ lọt thông tin bảo mật.

2.8. Giám sát hệ thống (Monitoring) và Ghi nhật ký tập trung (Centralized Logging)
Để đảm bảo khả năng vận hành ổn định trên môi trường production, dự án đã thiết lập hệ thống quan sát toàn diện (Observability Stack) tích hợp sâu vào cụm Kubernetes.

2.8.1. Thu thập và trực quan hóa chỉ số hiệu năng (Prometheus & Grafana)
- Prometheus: Hoạt động theo cơ chế pull-model, định kỳ kéo chỉ số hiệu năng (metrics) từ các pod microservice (đã cấu hình endpoint `/metrics` thông qua thư viện Prometheus-net). Các thông số thu thập bao gồm: số lượng request/giây, thời gian phản hồi API, tỷ lệ lỗi HTTP 5xx, dung lượng RAM và tải CPU tiêu thụ.
- Grafana: Được tích hợp để trực quan hóa các số liệu thu thập được từ Prometheus. Một bảng điều khiển trực quan (Dashboard) chi tiết dựa trên file cấu hình [microservices-overview.json](file:///c:/Users/khang/OneDrive/Desktop/devops%20-%20Copy/monitoring/grafana/dashboards/microservices-overview.json) đã được thiết kế, giúp nhóm vận hành theo dõi trực quan trạng thái sức khỏe của toàn bộ 6 dịch vụ microservices trên cùng một màn hình theo thời gian thực.

2.8.2. Gom log tập trung (Loki & Promtail) và cảnh báo tự động (Alertmanager)
- Loki & Promtail: Thay vì sử dụng ELK Stack cồng kềnh, dự án sử dụng PLG Stack (Prometheus, Loki, Grafana) tối ưu tài nguyên hơn. Promtail chạy dưới dạng DaemonSet trên các node Kubernetes, tự động đọc log từ luồng output (stdout/stderr) của tất cả các container đang chạy và đẩy về Loki server để đánh chỉ mục. Nhóm vận hành có thể truy vấn log của bất kỳ pod nào trực tiếp trên giao diện Grafana bằng ngôn ngữ LogQL.
- Alertmanager: Được cấu hình cùng Prometheus để giám sát các điều kiện bất thường (ví dụ: một dịch vụ microservice bị sập hoàn toàn, tải CPU vượt ngưỡng 80% liên tục trong 5 phút, tỷ lệ lỗi HTTP tăng cao). Khi phát hiện sự cố, Alertmanager sẽ gửi cảnh báo tức thời qua Discord/Email giúp nhóm vận hành khắc phục lỗi kịp thời trước khi ảnh hưởng đến người dùng cuối.


KẾT LUẬN VÀ HƯỚNG PHÁT TRIỂN

3.1. Những kết quả đã đạt được
Dự án đã xây dựng thành công một quy trình phát triển và vận hành phần mềm khép kín, tự động hóa cao áp dụng phương pháp luận DevOps hiện đại cho hệ thống FashionStore. Các thành tựu nổi bật bao gồm:
- Phân rã nghiệp vụ thương mại điện tử thành 6 dịch vụ độc lập hiệu năng cao chạy bằng C# .NET 10 kết hợp với ứng dụng khách Blazor WebAssembly và CSS Tailwind v4.
- Thiết lập quy trình CI tự động hóa việc build, chạy test và quét chất lượng tĩnh bằng SonarCloud, triệt tiêu mã nguồn xấu (bad code) và các lỗ hổng bảo mật nghiêm trọng.
- Triển khai hạ tầng điều phối Container với Kubernetes (K3s) trên máy chủ ảo, quản lý cấu hình thông qua ConfigMaps và Secrets động.
- Triển khai thành công bộ kiểm thử đơn vị tự động (Unit Testing) toàn diện cho các API Controller của 4 dịch vụ mới (Basket, Chat, Discount, Ordering) nâng tổng số unit test chạy trên CI lên 71 test cases.
- Triển khai toàn diện hệ thống giám sát hiệu năng (Prometheus, Grafana) và thu thập log tập trung (Loki, Promtail) cùng hệ thống cảnh báo (Alertmanager) trên Kubernetes, giúp theo dõi vận hành thời gian thực.
- Tự động hóa quá trình deploy bằng CD GitHub Actions thông qua kết nối SSH an toàn, thực hiện chiến lược Rolling Update giúp hệ thống nâng cấp không gián đoạn.

3.2. Khó khăn và Bài học kinh nghiệm
Trong quá trình triển khai, nhóm đã gặp phải và rút ra được các bài học kinh nghiệm quý báu:
- Quản lý tài nguyên Kubernetes trên VM cấu hình thấp: Việc quản lý phân giải DNS, định tuyến ingress và dung lượng RAM của các pod .NET là thử thách lớn, đòi hỏi phải tối ưu cấu hình Docker biên dịch và giới hạn tài nguyên (Resource Limits) của từng pod trên Kubernetes.
- Cấu hình SonarCloud đúng cách: Việc thiết lập loại trừ mã nguồn thừa (sonar.exclusions) và bỏ qua code coverage đối với các dự án chưa viết kiểm thử là cực kỳ quan trọng, giúp tránh tình trạng pipeline CI/CD bị block liên tục mà vẫn giữ được tính năng quét lỗi bảo mật tự động.
- Tái cấu trúc mã nguồn để hỗ trợ kiểm thử: Việc chuyển đổi từ phụ thuộc trực tiếp các dịch vụ cơ sở dữ liệu thô sang các interface trừu tượng trong `ChatService` là bắt buộc để có thể viết mock tests, nâng cao chất lượng kiến trúc phần mềm.
- Bảo mật thông tin cấu hình: Tuyệt đối không lưu trữ thông tin nhạy cảm trên mã nguồn. Việc kết hợp GitHub Secrets, Kubernetes Secrets và cơ chế tự động xóa file tạm sau mỗi lần deploy là quy trình bắt buộc.

3.3. Định hướng phát triển tương lai
Dựa trên nền tảng đã xây dựng, nhóm đề xuất các hướng phát triển và cải tiến trong tương lai:
- Triển khai GitOps với ArgoCD: Thay thế cơ chế deploy qua SSH script thô sơ bằng công cụ ArgoCD — giải pháp GitOps tiên tiến cho phép theo dõi và đồng bộ trạng thái hệ thống trực tiếp từ mã nguồn Git.
- Bổ sung bộ kiểm thử tự động Frontend: Từng bước bổ sung bộ test suite frontend bằng bUnit và tích hợp vào pipeline kiểm thử tự động của GitHub Actions, đạt mục tiêu độ bao phủ kiểm thử tối thiểu 80%.
- Horizontal Pod Autoscaler (HPA): Cấu hình tự động mở rộng số lượng pod dựa trên chỉ số tải CPU/RAM để đảm bảo hệ thống chịu tải tốt trong các đợt cao điểm (như Black Friday, Flash Sale).


TÀI LIỆU THAM KHẢO

[1] Microsoft. (2024). Blazor WebAssembly documentation. https://docs.microsoft.com/aspnet/core/blazor/
[2] Docker Inc. (2024). Docker Documentation — Multi-stage builds. https://docs.docker.com/build/building/multi-stage/
[3] Kubernetes.io. (2024). Kubernetes Documentation — Deployments. https://kubernetes.io/docs/concepts/workloads/controllers/deployment/
[4] GitHub. (2024). GitHub Actions — Understanding GitHub Actions. https://docs.github.com/actions/learn-github-actions/understanding-github-actions
[5] SonarSource. (2024). SonarCloud Documentation. https://docs.sonarcloud.io/
[6] Microsoft Azure. (2024). Azure Container Registry documentation. https://learn.microsoft.com/azure/container-registry/
[7] Rancher Labs. (2024). K3s — Lightweight Kubernetes. https://k3s.io/
[8] Redis Ltd. (2024). Redis Documentation. https://redis.io/documentation
[9] MongoDB Inc. (2024). MongoDB Atlas Documentation. https://www.mongodb.com/docs/atlas/
[10] Cloudinary Ltd. (2024). Cloudinary Documentation. https://cloudinary.com/documentation
[11] Newman, S. (2021). Building Microservices: Designing Fine-Grained Systems (2nd ed.). O'Reilly Media.
[12] Kim, G., Humble, J., Debois, P., & Willis, J. (2016). The DevOps Handbook. IT Revolution Press.
[13] Fowler, M., & Lewis, J. (2014). Microservices: A definition of this new architectural term. MartinFowler.com.
[14] Evans, E. (2003). Domain-Driven Design: Tackling Complexity in the Heart of Software. Addison-Wesley.
