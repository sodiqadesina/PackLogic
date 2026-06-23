# PackLogic Roadmap

PackLogic is a manufacturing packaging optimization platform designed to help operators, production teams, and shipping teams quickly determine the best plastic bag, shipping box, and packing arrangement for parts and multi-item orders.

This roadmap defines the professional delivery path from the current scaffolded repository to a working MVP and then to a production-ready platform.

---

## 1. Product Vision

Build a reliable digital packaging assistant that turns packaging decisions into a repeatable, data-driven workflow.

The system should help users answer:

> Given a part, its dimensions, quantity, handling rules, and available packaging inventory, what bag and box should be used, and how should the item be arranged?

---

## 2. Guiding Principles

- **Operator-first design:** The UI should be fast, simple, and usable on the shop floor.
- **Explainable recommendations:** Every recommendation should include the reason it was selected.
- **Deterministic MVP logic:** Early recommendations should be predictable and testable before introducing advanced optimization.
- **Clean architecture:** Domain rules should remain independent from UI, database, and infrastructure concerns.
- **Incremental delivery:** Build a useful single-part recommendation workflow first, then expand to multi-part optimization.
- **Data quality first:** Packaging recommendations are only as good as the part, bag, and box data entered into the system.

---

## 3. Current Repository Baseline

The repository currently contains the foundation for a full-stack application:

- ASP.NET Core Web API project
- Domain, Application, Infrastructure, and Optimization projects
- Unit test and integration test projects
- Angular client project
- Initial README describing the product vision

The next step is to replace scaffold code and placeholder classes with the real domain model, application services, optimization logic, API endpoints, and frontend workflows.

---

## 4. MVP Scope

The first production-style MVP should support the following workflow:

1. User selects or enters a part.
2. User enters quantity.
3. System calculates required bag size.
4. System calculates required box size after bagging.
5. System returns the best recommendation and alternative options.
6. System explains why the recommendation was chosen.
7. User can save the packaging decision for future reference.

### MVP In Scope

- Part catalog
- Bag catalog
- Box catalog
- Basic packaging rules
- Single-part recommendation workflow
- Quantity-based recommendation
- Utilization scoring
- Recommendation history
- REST API endpoints
- Angular user interface
- Unit tests for recommendation logic

### MVP Out of Scope

- Full 3D bin packing
- ERP integration
- Barcode scanning
- Authentication and role management
- Multi-site enterprise deployment
- AI-assisted recommendations

These items should be treated as later-phase enhancements after the MVP is reliable.

---

## 5. Target Architecture

```text
Angular Client
    |
    | REST / JSON
    v
PackLogic.Api
    |
    v
PackLogic.Application
    |
    | Uses domain rules and optimization services
    v
PackLogic.Domain + PackLogic.Optimization
    |
    v
PackLogic.Infrastructure
    |
    v
SQL Server
```

### Project Responsibilities

| Project | Responsibility |
| --- | --- |
| `PackLogic.Api` | HTTP endpoints, request/response contracts, API configuration, Swagger/OpenAPI |
| `PackLogic.Application` | Use cases, DTOs, validation, orchestration, service interfaces |
| `PackLogic.Domain` | Core entities, value objects, enums, business rules |
| `PackLogic.Infrastructure` | EF Core, SQL Server, repositories, persistence configuration |
| `PackLogic.Optimization` | Bag/box recommendation algorithms, utilization scoring, arrangement logic |
| `PackLogic.Client` | Angular UI, forms, dashboard, recommendation screens |
| `tests` | Unit tests, integration tests, regression coverage |

---

## 6. Delivery Phases

### Phase 0 — Project Foundation

**Goal:** Prepare the repository for serious development.

Deliverables:

- Clean solution structure
- Remove placeholder `Class1.cs` files
- Add `.gitignore`
- Add editor/config conventions
- Add initial documentation
- Confirm backend and frontend build locally
- Establish branching and commit conventions

Exit Criteria:

- `dotnet build` succeeds
- Angular app starts locally
- Repository has clear setup instructions
- Placeholder code is removed or replaced

---

### Phase 1 — Domain Model and Data Foundation

**Goal:** Model the real packaging business domain.

Deliverables:

- `Part` entity
- `Bag` entity
- `Box` entity
- `PackagingJob` entity
- `PackagingRecommendation` entity
- `Dimensions` value object
- `Weight` value object
- Rotation, clearance, stackability, and packaging rule enums
- EF Core `DbContext`
- SQL Server connection configuration
- Initial migrations
- Seed data for sample parts, bags, and boxes

Exit Criteria:

- Data model supports parts, bags, boxes, and saved recommendations
- Database can be created from migrations
- Seed data is available for testing the MVP workflow

---

### Phase 2 — Recommendation Engine MVP

**Goal:** Build the first useful bag and box selection algorithm.

Deliverables:

- Bag fit calculation
- Box fit calculation
- Quantity-based volume estimation
- Clearance allowance support
- Rotation support where allowed
- Weight limit validation
- Utilization scoring
- Alternative recommendations
- Reason codes explaining recommendations
- Unit tests for fit and scoring logic

Exit Criteria:

- Given part dimensions and quantity, the system returns the best bag and box
- Recommendations are deterministic and covered by unit tests
- Invalid or impossible packaging scenarios are handled clearly

---

### Phase 3 — Backend API

**Goal:** Expose the core functionality through clean REST endpoints.

Deliverables:

- Part CRUD endpoints
- Bag CRUD endpoints
- Box CRUD endpoints
- Recommendation endpoint
- Recommendation history endpoint
- DTOs and mapping logic
- Request validation
- Consistent error response format
- Swagger/OpenAPI documentation

Suggested API Endpoints:

```text
GET    /api/parts
POST   /api/parts
GET    /api/parts/{id}
PUT    /api/parts/{id}
DELETE /api/parts/{id}

GET    /api/bags
POST   /api/bags
GET    /api/boxes
POST   /api/boxes

POST   /api/recommendations
GET    /api/recommendations/history
GET    /api/recommendations/{id}
```

Exit Criteria:

- API can support the full MVP workflow
- Swagger clearly documents all request and response payloads
- Integration tests validate major endpoints

---

### Phase 4 — Angular MVP Frontend

**Goal:** Build a usable interface for shop-floor packaging decisions.

Deliverables:

- Application shell and routing
- Dashboard/home screen
- Part catalog screen
- Bag catalog screen
- Box catalog screen
- Packaging job form
- Recommendation results screen
- Alternative recommendations display
- Basic responsive layout
- Error and loading states

Exit Criteria:

- User can complete the MVP workflow from the browser
- Recommendation result is clear and actionable
- UI works with real API responses

---

### Phase 5 — Packaging Visualization

**Goal:** Make recommendations easier to follow visually.

Deliverables:

- 2D top-view layout
- 2D side-view layout
- Layer indication for stacked quantities
- Package utilization display
- Simple visual instructions for operators
- Export-ready packaging instruction format

Exit Criteria:

- User can see a simple visual representation of how the item fits
- Visualization reflects the selected box/bag recommendation
- Layout output can be used as an operator instruction

---

### Phase 6 — Quality, DevOps, and Release Readiness

**Goal:** Make the application maintainable and deployable.

Deliverables:

- Backend unit test coverage for core algorithms
- Backend integration tests for APIs
- Frontend component/service tests
- GitHub Actions CI workflow
- Docker support for local development
- Environment-specific configuration
- Health check endpoint
- Logging improvements
- Deployment documentation

Exit Criteria:

- Pull requests can be validated automatically
- App can be built consistently
- Local setup is documented and repeatable

---

### Phase 7 — Advanced Optimization

**Goal:** Improve recommendation intelligence beyond the MVP.

Deliverables:

- Multi-part order support
- Multi-layer packing logic
- More advanced 3D bin-packing heuristics
- Better rotation and orientation optimization
- Fragility and separation rules
- Packaging material cost scoring
- Inventory-aware recommendations

Exit Criteria:

- System can recommend packaging for more complex real-world orders
- Recommendations consider efficiency, rules, cost, and availability

---

### Phase 8 — Enterprise Features

**Goal:** Prepare the platform for organizational use.

Deliverables:

- Authentication
- Authorization and roles
- Audit logs
- Reporting dashboard
- PDF export
- Barcode scanning
- ERP/MRP integration readiness
- Admin configuration screens
- Multi-location support

Exit Criteria:

- System can support a controlled production pilot
- Admins can manage users, data, rules, and reports

---

## 7. Suggested Milestone Plan

| Milestone | Focus | Outcome |
| --- | --- | --- |
| M0 | Foundation | Repo builds, docs added, structure cleaned |
| M1 | Domain/Data | Parts, bags, boxes, and rules modeled |
| M2 | Engine MVP | Bag and box recommendation logic works |
| M3 | API MVP | Backend supports catalog and recommendation workflows |
| M4 | Frontend MVP | User can request recommendations from browser |
| M5 | Visualization | Basic layout/instruction output available |
| M6 | Release Prep | Tests, CI, Docker, documentation |
| M7 | Advanced Optimization | Multi-part and smarter packing support |
| M8 | Enterprise Pilot | Auth, reporting, audit, export, integrations |

---

## 8. Technical Standards

### Backend Standards

- Use clean architecture boundaries.
- Keep domain entities free of infrastructure dependencies.
- Keep optimization logic testable and deterministic.
- Use DTOs for API contracts.
- Use validation before executing use cases.
- Use structured error responses.
- Add tests for every algorithm edge case.

### Frontend Standards

- Use Angular standalone components where appropriate.
- Keep API access inside services.
- Use typed request and response models.
- Keep forms validated and operator-friendly.
- Display recommendation explanations, not just results.
- Design for speed and clarity on production floors.

### Data Standards

- Store dimensions in a consistent unit.
- Store weight in a consistent unit.
- Avoid hard-coded packaging sizes in business logic.
- Treat bags and boxes as configurable catalog data.
- Track recommendation history for auditability.

---

## 9. Definition of Done

A feature is considered done when:

- It satisfies the user workflow.
- It has appropriate validation.
- It has meaningful test coverage.
- It handles error cases.
- It is documented where needed.
- It does not break existing build or tests.
- It follows the repository architecture.

---

## 10. Long-Term Product Direction

The long-term goal is to turn PackLogic into a production-ready packaging optimization platform that can reduce decision time, improve consistency, lower material waste, and support manufacturing teams with intelligent, repeatable packaging decisions.
