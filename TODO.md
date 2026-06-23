# PackLogic Developer TODO

This TODO list converts the PackLogic roadmap into an actionable developer backlog. It is organized by priority and delivery phase so the project can move from scaffold to MVP in a professional, controlled way.

---

## Status Legend

- `[ ]` Not started
- `[~]` In progress
- `[x]` Complete
- `[!]` Blocked or needs decision

---

## Immediate Priority

These tasks should be completed first because they create the foundation for all future work.

### Repository and Project Setup

- [ ] Confirm `dotnet build` succeeds from the solution root.
- [ ] Confirm the Angular client starts with `npm install` and `npm start`.
- [ ] Add or verify root `.gitignore` for .NET, Angular, Visual Studio, and environment files.
- [ ] Remove placeholder `Class1.cs` files once real classes are added.
- [ ] Add root setup instructions to `README.md`.
- [ ] Add local development requirements:
  - [ ] .NET SDK version
  - [ ] Node.js version
  - [ ] Angular CLI usage
  - [ ] SQL Server requirement
- [ ] Decide the default database strategy for local development:
  - [ ] SQL Server LocalDB
  - [ ] SQL Server Express
  - [ ] Docker SQL Server container
- [ ] Add consistent naming conventions for entities, DTOs, services, and endpoints.

### Architecture Cleanup

- [ ] Confirm project references follow clean architecture rules.
- [ ] Ensure `PackLogic.Domain` has no dependency on other application projects.
- [ ] Ensure `PackLogic.Application` references `PackLogic.Domain` only where needed.
- [ ] Ensure `PackLogic.Infrastructure` handles EF Core and persistence.
- [ ] Ensure `PackLogic.Api` depends on Application, Infrastructure, and Optimization through service registration.
- [ ] Create dependency injection extension methods:
  - [ ] `AddApplicationServices()`
  - [ ] `AddInfrastructureServices()`
  - [ ] `AddOptimizationServices()`

---

## Phase 1 — Domain Model

### Value Objects

- [ ] Create `Dimensions` value object.
  - [ ] Length
  - [ ] Width
  - [ ] Height
  - [ ] Unit of measure
  - [ ] Volume calculation
  - [ ] Validation for positive values
- [ ] Create `Weight` value object.
  - [ ] Value
  - [ ] Unit of measure
  - [ ] Validation for non-negative values
- [ ] Create `Money` value object if packaging cost tracking is needed later.

### Enums

- [ ] Create `DimensionUnit` enum.
- [ ] Create `WeightUnit` enum.
- [ ] Create `RotationPolicy` enum.
- [ ] Create `StackabilityRule` enum.
- [ ] Create `PackagingType` enum.
- [ ] Create `RecommendationStatus` enum.

### Core Entities

- [ ] Create `Part` entity.
  - [ ] Id
  - [ ] Part number
  - [ ] Description
  - [ ] Dimensions
  - [ ] Weight
  - [ ] Rotation policy
  - [ ] Stackability rule
  - [ ] Is active
- [ ] Create `Bag` entity.
  - [ ] Id
  - [ ] Bag code/SKU
  - [ ] Description
  - [ ] Usable dimensions
  - [ ] Material/type
  - [ ] Clearance allowance
  - [ ] Is active
- [ ] Create `Box` entity.
  - [ ] Id
  - [ ] Box code/SKU
  - [ ] Description
  - [ ] Internal dimensions
  - [ ] Max weight
  - [ ] Is active
- [ ] Create `PackagingJob` entity.
  - [ ] Id
  - [ ] Part reference
  - [ ] Quantity
  - [ ] Requested date
  - [ ] Notes
- [ ] Create `PackagingRecommendation` entity.
  - [ ] Id
  - [ ] Packaging job reference
  - [ ] Recommended bag
  - [ ] Recommended box
  - [ ] Utilization score
  - [ ] Explanation
  - [ ] Created timestamp

---

## Phase 2 — Infrastructure and Database

### EF Core Setup

- [ ] Add EF Core packages to `PackLogic.Infrastructure`.
- [ ] Create `PackLogicDbContext`.
- [ ] Add `DbSet<Part>`.
- [ ] Add `DbSet<Bag>`.
- [ ] Add `DbSet<Box>`.
- [ ] Add `DbSet<PackagingJob>`.
- [ ] Add `DbSet<PackagingRecommendation>`.
- [ ] Configure entity mappings using Fluent API.
- [ ] Configure value object ownership for dimensions and weight.
- [ ] Add SQL Server connection string to `appsettings.Development.json`.
- [ ] Register DbContext in dependency injection.
- [ ] Create first EF migration.
- [ ] Verify database creation locally.

### Seed Data

- [ ] Add initial sample parts.
- [ ] Add initial sample bag sizes.
- [ ] Add initial sample box sizes.
- [ ] Add a development-only seed process.
- [ ] Document how to reset local seed data.

### Repository Pattern / Data Access

- [ ] Decide whether to use direct DbContext in Application services or repository abstractions.
- [ ] If using repositories, create:
  - [ ] `IPartRepository`
  - [ ] `IBagRepository`
  - [ ] `IBoxRepository`
  - [ ] `IPackagingRecommendationRepository`
- [ ] Implement repositories in Infrastructure.
- [ ] Add basic data access tests.

---

## Phase 3 — Recommendation Engine MVP

### Core Models

- [ ] Create `PackagingRequest` model.
- [ ] Create `PackagingResult` model.
- [ ] Create `BagRecommendation` model.
- [ ] Create `BoxRecommendation` model.
- [ ] Create `RecommendationAlternative` model.
- [ ] Create `RecommendationReason` model.

### Bag Recommendation Logic

- [ ] Implement basic bag fit check.
- [ ] Support clearance allowance.
- [ ] Support part rotation where allowed.
- [ ] Reject inactive bags.
- [ ] Rank bags by lowest wasted space.
- [ ] Return top alternative bags.
- [ ] Add reason text explaining why the selected bag fits.
- [ ] Add unit tests for:
  - [ ] Exact fit
  - [ ] Fit with clearance
  - [ ] No available bag
  - [ ] Rotation allowed
  - [ ] Rotation not allowed

### Box Recommendation Logic

- [ ] Implement basic box fit check.
- [ ] Use bagged dimensions when bag recommendation exists.
- [ ] Support quantity-based packing estimate.
- [ ] Validate max box weight.
- [ ] Reject inactive boxes.
- [ ] Rank boxes by utilization score.
- [ ] Return top alternative boxes.
- [ ] Add reason text explaining why the selected box fits.
- [ ] Add unit tests for:
  - [ ] Single item fit
  - [ ] Multiple quantity fit
  - [ ] Weight exceeds box limit
  - [ ] No available box
  - [ ] Alternative box ranking

### Utilization Scoring

- [ ] Create utilization calculation service.
- [ ] Calculate bag utilization.
- [ ] Calculate box utilization.
- [ ] Define scoring formula.
- [ ] Add tests for scoring edge cases.

---

## Phase 4 — Application Layer

### DTOs

- [ ] Create `PartDto`.
- [ ] Create `CreatePartRequest`.
- [ ] Create `UpdatePartRequest`.
- [ ] Create `BagDto`.
- [ ] Create `CreateBagRequest`.
- [ ] Create `BoxDto`.
- [ ] Create `CreateBoxRequest`.
- [ ] Create `CreatePackagingRecommendationRequest`.
- [ ] Create `PackagingRecommendationResponse`.

### Services

- [ ] Create `IPartService`.
- [ ] Create `IBagService`.
- [ ] Create `IBoxService`.
- [ ] Create `IPackagingRecommendationService`.
- [ ] Implement part management service.
- [ ] Implement bag management service.
- [ ] Implement box management service.
- [ ] Implement recommendation orchestration service.

### Validation

- [ ] Validate required fields.
- [ ] Validate positive dimensions.
- [ ] Validate positive quantity.
- [ ] Validate duplicate part numbers.
- [ ] Validate duplicate bag/box codes.
- [ ] Add user-friendly validation messages.

---

## Phase 5 — Backend API

### API Foundation

- [ ] Remove default `/weatherforecast` endpoint.
- [ ] Add API route grouping under `/api`.
- [ ] Add controller or minimal API structure decision.
- [ ] Add global exception handling.
- [ ] Add consistent API response format.
- [ ] Enable Swagger/OpenAPI in development.
- [ ] Add CORS policy for Angular client.

### Part Endpoints

- [ ] `GET /api/parts`
- [ ] `GET /api/parts/{id}`
- [ ] `POST /api/parts`
- [ ] `PUT /api/parts/{id}`
- [ ] `DELETE /api/parts/{id}` or soft delete

### Bag Endpoints

- [ ] `GET /api/bags`
- [ ] `GET /api/bags/{id}`
- [ ] `POST /api/bags`
- [ ] `PUT /api/bags/{id}`
- [ ] `DELETE /api/bags/{id}` or soft delete

### Box Endpoints

- [ ] `GET /api/boxes`
- [ ] `GET /api/boxes/{id}`
- [ ] `POST /api/boxes`
- [ ] `PUT /api/boxes/{id}`
- [ ] `DELETE /api/boxes/{id}` or soft delete

### Recommendation Endpoints

- [ ] `POST /api/recommendations`
- [ ] `GET /api/recommendations/history`
- [ ] `GET /api/recommendations/{id}`
- [ ] Add integration tests for recommendation endpoint.

---

## Phase 6 — Angular Frontend MVP

### App Foundation

- [ ] Confirm Angular routing setup.
- [ ] Add base layout.
- [ ] Add navigation menu.
- [ ] Add API environment configuration.
- [ ] Create shared API service.
- [ ] Create shared loading and error components.

### Screens

- [ ] Dashboard screen.
- [ ] Part catalog list screen.
- [ ] Create/edit part form.
- [ ] Bag catalog list screen.
- [ ] Create/edit bag form.
- [ ] Box catalog list screen.
- [ ] Create/edit box form.
- [ ] Packaging recommendation form.
- [ ] Recommendation result screen.
- [ ] Recommendation history screen.

### User Experience

- [ ] Make recommendation workflow fast and simple.
- [ ] Show selected bag clearly.
- [ ] Show selected box clearly.
- [ ] Show utilization percentage.
- [ ] Show explanation/reason codes.
- [ ] Show alternative bag and box options.
- [ ] Add form validation messages.
- [ ] Add empty states for catalogs.

---

## Phase 7 — Testing and Quality

### Backend Tests

- [ ] Add unit tests for domain value objects.
- [ ] Add unit tests for bag recommendation logic.
- [ ] Add unit tests for box recommendation logic.
- [ ] Add unit tests for utilization scoring.
- [ ] Add unit tests for validation rules.
- [ ] Add integration tests for major API endpoints.

### Frontend Tests

- [ ] Add tests for API services.
- [ ] Add tests for recommendation form validation.
- [ ] Add tests for result rendering.
- [ ] Add tests for empty and error states.

### CI/CD

- [ ] Add GitHub Actions workflow for backend build.
- [ ] Add GitHub Actions workflow for backend tests.
- [ ] Add GitHub Actions workflow for Angular build.
- [ ] Add status badge to README after CI is configured.

---

## Phase 8 — Visualization

### 2D MVP Visualization

- [ ] Define simple layout data model.
- [ ] Generate top-view placement data.
- [ ] Generate side-view placement data.
- [ ] Display item position inside selected box.
- [ ] Display quantity/layer information.
- [ ] Add utilization label to visualization.

### Future 3D Visualization

- [ ] Evaluate Three.js integration.
- [ ] Define 3D scene model.
- [ ] Render box and item geometry.
- [ ] Support item rotation preview.
- [ ] Support layer visualization.

---

## Phase 9 — Production Readiness

### Security and Access

- [ ] Add authentication strategy.
- [ ] Add authorization roles:
  - [ ] Admin
  - [ ] Supervisor
  - [ ] Operator
- [ ] Protect admin catalog management routes.
- [ ] Add audit fields to important entities.

### Reporting

- [ ] Add recommendation history filters.
- [ ] Add packaging usage report.
- [ ] Add top-used bags report.
- [ ] Add top-used boxes report.
- [ ] Add wasted-space/efficiency report.
- [ ] Add PDF export for recommendation instructions.

### Operations

- [ ] Add health check endpoint.
- [ ] Add structured logging.
- [ ] Add Docker Compose for local development.
- [ ] Add deployment guide.
- [ ] Add environment variable documentation.

---

## First Development Sprint Recommendation

The first focused sprint should complete the foundation needed before writing business logic.

### Sprint 1 Goal

Prepare PackLogic for domain-driven backend development.

### Sprint 1 Tasks

- [ ] Clean placeholder files.
- [ ] Confirm backend build.
- [ ] Confirm frontend build.
- [ ] Add `.gitignore` if missing.
- [ ] Add `Dimensions` value object.
- [ ] Add `Weight` value object.
- [ ] Add `Part` entity.
- [ ] Add `Bag` entity.
- [ ] Add `Box` entity.
- [ ] Add first unit tests for value object validation.

### Sprint 1 Deliverable

A clean backend foundation with the first real domain objects committed and tested.

---

## MVP Completion Checklist

The MVP is considered complete when:

- [ ] Parts can be created and managed.
- [ ] Bags can be created and managed.
- [ ] Boxes can be created and managed.
- [ ] User can submit a packaging recommendation request.
- [ ] System recommends a bag.
- [ ] System recommends a box.
- [ ] System shows utilization score.
- [ ] System explains why the recommendation was selected.
- [ ] Recommendation can be saved.
- [ ] User can view recommendation history.
- [ ] Angular frontend supports the complete workflow.
- [ ] Core recommendation logic has unit tests.
- [ ] Main API endpoints have integration tests.
- [ ] README explains how to run the project locally.
