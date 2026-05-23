# Serious Fashion - Devops Project

This repository contains the source code, Docker configurations, Kubernetes manifests, and CI/CD pipelines for **Serious Fashion**, a microservices-based e-commerce platform.

## Project Structure

The project is split into three main components:
- **`blazor_frontend`**: Blazor WebAssembly single-page application (SPA) serving as the storefront.
- **`serious`**: A set of .NET 10 microservices implementing the e-commerce business logic.
- **`k8s`**: Kubernetes manifests for deploying the entire stack to staging and production environments.

---

## 1. Frontend (`blazor_frontend`)
A Blazor WebAssembly SPA containing the storefront, admin dashboards, staff order management, discount management, and real-time support chat.

### Tech Stack
- **Framework**: Blazor WebAssembly (.NET 10)
- **Styling**: Tailwind CSS v4
- **Real-time communication**: SignalR Client for live chat support.

### Local Development Setup
1. **Prerequisites**: Install [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) and [Node.js 20+](https://nodejs.org/).
2. **Install frontend dependencies** (required for Tailwind build step):
   ```bash
   cd blazor_frontend/blazor_frontend
   npm install
   ```
3. **Build and Run**:
   ```bash
   dotnet run
   ```

---

## 2. Microservices Backend (`serious`)
The backend is built as a set of containerized .NET microservices communicating via HTTP/REST and WebSockets:

### Backend Services
| Service | Internal Port | Description | Database / Cache / Media |
| :--- | :---: | :--- | :--- |
| **`identity-service`** | `7093` | User registration, authentication, authorization, and roles (Admin/Staff/User). | SQL Database, Cloudinary, SMTP |
| **`catalog-service`** | `7103` | Product catalog, category management, inventory, and reviews. | SQL Database, Cloudinary |
| **`ordering-service`** | `7076` | Checkout processing, order placement, status tracking, and order history. | SQL Database |
| **`basket-service`** | `7021` | Shopping cart persistence, guest cart merging upon login. | Redis Cache |
| **`discount-service`** | `7002` | Promo codes, discount rules, and percentage/fixed discounts. | MongoDB |
| **`chat-service`** | `7229` | Real-time messaging hub between customers, guests, and support staff. | MongoDB, Redis Cache |

### Local Development Setup (Docker Compose)
1. Configure your local environment variables in a `.env` file at the root of `serious/`.
2. Start all databases and services in development mode:
   ```bash
   cd serious
   docker-compose up --build
   ```

---

## 3. Deployment & CI/CD (`.github/workflows` & `k8s`)

The project uses GitHub Actions workflows for continuous integration and automated deployment:

### CI/CD Pipelines
- **`dotnet-ci.yml`**: Compiles C# projects, runs unit tests, runs SonarCloud analysis, builds Docker images, and pushes them to Azure Container Registry (ACR).
- **`frontend-ci.yml`**: Compiles the Blazor WebAssembly client, compiles Tailwind CSS, triggers SonarCloud analysis with code coverage and library exclusions, builds the SPA container, and pushes it to ACR.
- **`cd.yml`**: Triggers upon successful build jobs. Copies the Kubernetes manifests to the VM, generates config secrets, applies deployments/ingresses to the K3s cluster, and performs rollouts to `staging` or `production` namespaces.

### Kubernetes Manifests (`k8s`)
Contains the resources definitions needed for cluster deployment:
- `config/`: Configurations, environment maps, and service ports.
- `backend/`: Microservices deployment specs, pod scaling, and container specs.
- `frontend/`: Blazor WebAssembly static server (Nginx container) deployment specs.
- `ingress/`: Ingress controllers routing `/` to the frontend and `/api/*` to the respective microservices.
