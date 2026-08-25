# PackLogic Developer TODO

This TODO list converts the PackLogic roadmap and README into an actionable developer backlog. It is organized by priority and delivery phase so the project can move from the current scaffold to a geometry-driven, weight-aware MVP and then toward advanced irregular-shape, CAD, and enterprise functionality.

---

## Status Legend

- `[ ]` Not started
- `[~]` In progress
- `[x]` Complete
- `[!]` Blocked or needs decision

---

# Immediate Priority

These tasks should be completed first because they define the domain foundation that every future packaging recommendation depends on.

## Repository and Project Setup

- [x] Confirm `dotnet build` succeeds from the solution root.
- [x] Confirm the Angular client starts with `npm install` and `npm start`.
- [x] Add or verify root `.gitignore` for .NET, Angular, Visual Studio, and environment files.
- [x] Remove placeholder `Class1.cs` files or confirm they are no longer present.
- [ ] Add root local setup instructions to `README.md`.
- [ ] Document local development requirements:
  - [ ] .NET SDK version
  - [ ] Node.js version
  - [ ] npm version
  - [ ] Angular CLI usage
  - [ ] SQL Server requirement
- [ ] Decide the default database strategy for local development:
  - [ ] SQL Server LocalDB
  - [ ] SQL Server Express
  - [ ] Docker SQL Server container
- [ ] Align Entity Framework Core package versions with the selected .NET target version.
- [ ] Add consistent naming conventions for:
  - [ ] Entities
  - [ ] Value objects
  - [ ] Geometry models
  - [ ] DTOs
  - [ ] Services
  - [ ] Optimization interfaces
  - [ ] API endpoints

## Architecture Cleanup

- [x] Create `AddApplicationServices()`.
- [x] Create `AddInfrastructureServices()`.
- [x] Create `AddOptimizationServices()`.
- [ ] Confirm project references follow clean architecture rules.
- [ ] Ensure `PackLogic.Domain` has no dependency on Application, Infrastructure, Optimization, or API.
- [ ] Ensure `PackLogic.Application` references `PackLogic.Domain` only where needed.
- [ ] Ensure `PackLogic.Infrastructure` handles EF Core, SQL Server, repositories, and external adapters.
- [ ] Ensure `PackLogic.Optimization` contains geometry/packing algorithms and does not depend on Infrastructure.
- [ ] Ensure `PackLogic.Api` composes Application, Infrastructure, and Optimization through dependency injection.

---

# Phase 1 — Geometry, Measurement, Weight, and Domain Foundation

## Core Enums

- [ ] Create `GeometryType` enum.
  - [ ] `Cuboid`
  - [ ] `ExtrudedProfile`
  - [ ] `Mesh` for future use
- [ ] Create `GeometrySource` enum.
  - [ ] `ManualDimensions`
  - [ ] `ShapeEditor`
  - [ ] `EngineeringDrawing`
  - [ ] `DxfImport`
  - [ ] `CadImport`
  - [ ] `SavedPart`
  - [ ] `ErpIntegration`
- [ ] Create `DimensionUnit` enum.
  - [ ] `Millimetre`
  - [ ] `Centimetre`
  - [ ] `Metre`
  - [ ] `Inch`
  - [ ] `Foot`
- [ ] Create `WeightUnit` enum.
  - [ ] `Milligram`
  - [ ] `Gram`
  - [ ] `Kilogram`
  - [ ] `Ounce`
  - [ ] `Pound`
- [ ] Create `RotationPolicy` enum.
- [ ] Create `StackabilityRule` enum.
- [ ] Create `PackagingType` enum.
- [ ] Create `RecommendationStatus` enum.

## Measurement Value Objects

- [ ] Create `Dimensions` value object.
  - [ ] Length
  - [ ] Width
  - [ ] Height
  - [ ] Dimension unit
  - [ ] Positive-value validation
  - [ ] Volume calculation
  - [ ] Conversion to normalized dimensions
- [ ] Create `Weight` value object.
  - [ ] Value
  - [ ] Weight unit
  - [ ] Non-negative validation
  - [ ] Conversion to normalized weight
- [ ] Create `Clearance` value object if useful.
  - [ ] Value
  - [ ] Dimension unit
  - [ ] Non-negative validation
- [ ] Create `Money` value object later if packaging cost scoring is added.

## Unit Conversion

- [ ] Create `IUnitConversionService` or equivalent abstraction.
- [ ] Implement dimension conversion.
  - [ ] mm to cm
  - [ ] cm to mm
  - [ ] mm to m
  - [ ] m to mm
  - [ ] in to mm
  - [ ] mm to in
  - [ ] ft to mm
  - [ ] mm to ft
- [ ] Implement weight conversion.
  - [ ] mg to kg
  - [ ] g to kg
  - [ ] lb to kg
  - [ ] oz to kg
  - [ ] reverse display conversions where required
- [ ] Define internal normalized dimension unit.
  - [ ] Millimetres
- [ ] Define internal normalized weight unit.
  - [ ] Kilograms
- [ ] Add precision/tolerance rules for conversions.
- [ ] Add unit tests for every supported conversion.

## Geometry Value Objects and Models

- [ ] Create `Point2D` value object.
  - [ ] X
  - [ ] Y
  - [ ] Equality
  - [ ] Validation
- [ ] Create `LineSegment` model/value object.
  - [ ] Start point
  - [ ] End point
  - [ ] Length calculation
- [ ] Create `ArcSegment` model/value object.
  - [ ] Start point
  - [ ] End point
  - [ ] Radius or arc-definition properties
- [ ] Design a profile representation that can support:
  - [ ] Straight edges
  - [ ] Arcs
  - [ ] Future tessellation
- [ ] Create base `PartGeometry` abstraction/model.
  - [ ] Geometry type
  - [ ] Geometry source
  - [ ] Dimension unit
- [ ] Create `CuboidGeometry`.
  - [ ] Length
  - [ ] Width
  - [ ] Height
  - [ ] Bounding dimensions
- [ ] Create `ExtrudedProfileGeometry`.
  - [ ] 2D profile
  - [ ] Vertices/segments
  - [ ] Extrusion depth
  - [ ] Bounding dimensions
- [ ] Reserve design path for future `MeshGeometry`.

## Geometry Validation

- [ ] Create geometry validator abstraction.
- [ ] Validate cuboid dimensions are positive.
- [ ] Validate irregular profile has enough points.
- [ ] Validate polygon closes correctly.
- [ ] Reject zero-length edges.
- [ ] Reject duplicate consecutive vertices.
- [ ] Detect self-intersections.
- [ ] Ensure polygon area is greater than zero.
- [ ] Validate extrusion depth is positive.
- [ ] Add validation tests for valid and invalid shapes.

## Geometry Normalization

- [ ] Create geometry normalizer abstraction.
- [ ] Translate polygon minimum X to `0`.
- [ ] Translate polygon minimum Y to `0`.
- [ ] Preserve shape dimensions after translation.
- [ ] Normalize imported coordinates into internal dimension units.
- [ ] Add normalization tests.

## Core Part Model

- [ ] Create `Part` entity.
  - [ ] Id
  - [ ] Optional part number
  - [ ] Description/name
  - [ ] Geometry reference/value
  - [ ] Weight
  - [ ] Rotation policy
  - [ ] Stackability rule
  - [ ] Clearance/handling rules
  - [ ] Is active
- [ ] Ensure part number is not required for a packaging recommendation.
- [ ] Support temporary/new unknown parts.
- [ ] Support saved catalog parts.

## Bag Entity

- [ ] Create `Bag` entity.
  - [ ] Id
  - [ ] Bag code/SKU
  - [ ] Description
  - [ ] Usable dimensions
  - [ ] Dimension unit
  - [ ] Material/type
  - [ ] Clearance allowance
  - [ ] Empty bag weight if known
  - [ ] Weight unit if applicable
  - [ ] Is active

## Box Entity

- [ ] Create `Box` entity.
  - [ ] Id
  - [ ] Box code/SKU
  - [ ] Description
  - [ ] Internal dimensions
  - [ ] Dimension unit
  - [ ] Maximum supported weight
  - [ ] Weight unit
  - [ ] Empty box weight if known
  - [ ] Is active

## Packaging Job Entity

- [ ] Create `PackagingJob` entity.
  - [ ] Id
  - [ ] Optional saved-part reference
  - [ ] Submitted geometry/reference
  - [ ] Product weight
  - [ ] Quantity
  - [ ] Requested date
  - [ ] Packaging constraints
  - [ ] Notes

## Packaging Recommendation Entity

- [ ] Create `PackagingRecommendation` entity.
  - [ ] Id
  - [ ] Packaging job reference
  - [ ] Geometry version reference where applicable
  - [ ] Recommended bag
  - [ ] Recommended box
  - [ ] Space utilization score
  - [ ] Weight utilization score
  - [ ] Total product weight
  - [ ] Estimated gross package weight
  - [ ] Remaining weight capacity
  - [ ] Explanation/reason codes
  - [ ] Created timestamp

## Geometry Versioning

- [ ] Design geometry version model.
- [ ] Allow saved part to reference multiple geometry versions.
- [ ] Ensure packaging recommendation references the geometry version used.
- [ ] Preserve historical traceability.

## Phase 1 Tests

- [ ] Add unit tests for `Dimensions`.
- [ ] Add unit tests for `Weight`.
- [ ] Add unit tests for unit conversions.
- [ ] Add unit tests for `Point2D`.
- [ ] Add unit tests for geometry validation.
- [ ] Add unit tests for geometry normalization.
- [ ] Add unit tests for cuboid geometry.
- [ ] Add unit tests for extruded-profile geometry.

---

# Phase 2 — Infrastructure and Database

## EF Core Setup

- [ ] Align EF Core packages with the project target framework.
- [ ] Add required EF Core SQL Server packages to `PackLogic.Infrastructure`.
- [ ] Create `PackLogicDbContext`.
- [ ] Add `DbSet<Part>`.
- [ ] Add geometry persistence model/configuration.
- [ ] Add geometry version persistence.
- [ ] Add `DbSet<Bag>`.
- [ ] Add `DbSet<Box>`.
- [ ] Add `DbSet<PackagingJob>`.
- [ ] Add `DbSet<PackagingRecommendation>`.
- [ ] Configure entity mappings using Fluent API.
- [ ] Configure value-object persistence.
- [ ] Decide whether irregular geometry is stored as:
  - [ ] JSON column
  - [ ] Separate related tables
  - [ ] Hybrid model
- [ ] Add SQL Server connection string to `appsettings.Development.json`.
- [ ] Register DbContext in `AddInfrastructureServices()`.
- [ ] Create first EF migration.
- [ ] Verify local database creation.

## Seed Data

- [ ] Add sample regular parts.
- [ ] Add sample irregular geometry records if useful.
- [ ] Add sample bag sizes.
- [ ] Add sample box sizes.
- [ ] Add sample box weight capacities.
- [ ] Include mixed unit examples for development testing.
- [ ] Add development-only seed process.
- [ ] Document how to reset local seed data.

## Data Access

- [ ] Decide between direct DbContext usage in Application or repository abstractions.
- [ ] If repositories are used, create:
  - [ ] `IPartRepository`
  - [ ] `IBagRepository`
  - [ ] `IBoxRepository`
  - [ ] `IPackagingRecommendationRepository`
  - [ ] geometry-version repository if required
- [ ] Implement repositories in Infrastructure.
- [ ] Add data-access tests.

---

# Phase 3 — Regular Packing Engine

## Core Optimization Models

- [ ] Create `PackagingRequest` model.
  - [ ] Geometry
  - [ ] Weight
  - [ ] Quantity
  - [ ] Rotation constraints
  - [ ] Stackability constraints
  - [ ] Clearance
- [ ] Create `PackagingResult` model.
- [ ] Create `PackingPlacement` model.
- [ ] Create `BagRecommendation` model.
- [ ] Create `BoxRecommendation` model.
- [ ] Create `RecommendationAlternative` model.
- [ ] Create `RecommendationReason` model.

## Cuboid Orientation Logic

- [ ] Generate all valid cuboid orientations.
- [ ] Respect rotation policy.
- [ ] Eliminate duplicate orientations for equal dimensions.
- [ ] Add orientation tests.

## Regular Geometric Fit

- [ ] Implement cuboid-within-box fit check.
- [ ] Apply clearance before fit validation.
- [ ] Validate package boundaries.
- [ ] Support quantity-based regular packing estimate.
- [ ] Add tests for exact fit.
- [ ] Add tests for fit after rotation.
- [ ] Add tests for failed fit.
- [ ] Add tests for clearance-caused failure.

## Weight Validation

- [ ] Create weight validation service.
- [ ] Calculate total product weight.
  - [ ] Part weight × quantity
- [ ] Validate total product weight against box maximum capacity.
- [ ] Add optional empty-box weight when gross weight is modeled.
- [ ] Add future packaging material weight hook.
- [ ] Calculate remaining box weight capacity.
- [ ] Calculate weight utilization percentage.
- [ ] Add tests for:
  - [ ] Exact maximum weight
  - [ ] Below maximum weight
  - [ ] Above maximum weight
  - [ ] Unit conversion during validation

## Quantity Splitting

- [ ] Detect when all units cannot fit geometrically in one box.
- [ ] Detect when all units exceed box weight capacity.
- [ ] Generate valid quantity splits.
- [ ] Compare split alternatives.
- [ ] Return required box count.
- [ ] Add tests for geometry-limited split.
- [ ] Add tests for weight-limited split.

## Utilization Scoring

- [ ] Create space-utilization calculation service.
- [ ] Create weight-utilization calculation service.
- [ ] Define ranking strategy that considers:
  - [ ] Space utilization
  - [ ] Weight utilization
  - [ ] Number of boxes
  - [ ] Future packaging cost
- [ ] Add scoring edge-case tests.

---

# Phase 4 — Bag Recommendation Engine

## Bag Fit Logic

- [ ] Implement basic bag fit check.
- [ ] Support selected dimension units.
- [ ] Normalize bag and part measurements before comparison.
- [ ] Support clearance allowance.
- [ ] Support part rotation where allowed.
- [ ] Reject inactive bags.
- [ ] Rank bags by lowest wasted space.
- [ ] Return top alternative bags.
- [ ] Add explanation/reason text.

## Bag Tests

- [ ] Exact fit.
- [ ] Fit with clearance.
- [ ] No available bag.
- [ ] Rotation allowed.
- [ ] Rotation not allowed.
- [ ] Mixed input units.
- [ ] Inactive bag rejection.

---

# Phase 5 — Box Recommendation Engine

## Box Fit Logic

- [ ] Implement box fit check using effective packaged geometry.
- [ ] Use bagged/effective dimensions when applicable.
- [ ] Normalize units before comparison.
- [ ] Support quantity-based packing.
- [ ] Validate box maximum weight.
- [ ] Reject inactive boxes.
- [ ] Rank boxes by space utilization.
- [ ] Rank boxes by weight utilization.
- [ ] Return alternative boxes.
- [ ] Return number of boxes required.
- [ ] Add reason text explaining why the selected box is valid.

## Effective Packaged Geometry

- [ ] Model bagging/protection clearance.
- [ ] Expand geometry where required.
- [ ] Preserve actual product geometry separately from effective packing geometry.
- [ ] Add tests for clearance-expanded geometry.

## Effective Packaged Weight

- [ ] Start with total product weight.
- [ ] Add optional empty-box weight support.
- [ ] Add optional bag weight support.
- [ ] Reserve future hooks for:
  - [ ] Foam
  - [ ] Bubble wrap
  - [ ] Dividers
  - [ ] Inserts
- [ ] Calculate estimated gross packaged weight when data is available.

## Box Tests

- [ ] Single-item fit.
- [ ] Multiple-quantity fit.
- [ ] Weight exceeds box limit.
- [ ] Geometry fits but weight fails.
- [ ] Weight fits but geometry fails.
- [ ] No available box.
- [ ] Alternative box ranking.
- [ ] Quantity split by weight.
- [ ] Quantity split by geometry.
- [ ] Mixed unit input.

---

# Phase 6 — Application Layer

## DTOs

- [ ] Create geometry DTOs.
- [ ] Create cuboid geometry DTO.
- [ ] Create extruded-profile geometry DTO.
- [ ] Create point/segment DTOs.
- [ ] Create weight DTO.
- [ ] Create `PartDto`.
- [ ] Create `CreatePartRequest`.
- [ ] Create `UpdatePartRequest`.
- [ ] Create `BagDto`.
- [ ] Create `CreateBagRequest`.
- [ ] Create `BoxDto`.
- [ ] Create `CreateBoxRequest`.
- [ ] Create `CreatePackagingRecommendationRequest`.
- [ ] Create `PackagingRecommendationResponse`.
- [ ] Include selected units in request/response contracts where appropriate.

## Services

- [ ] Create `IPartService`.
- [ ] Create `IBagService`.
- [ ] Create `IBoxService`.
- [ ] Create `IGeometryValidationService` if Application orchestration requires one.
- [ ] Create `IPackagingRecommendationService`.
- [ ] Implement part management service.
- [ ] Implement bag management service.
- [ ] Implement box management service.
- [ ] Implement recommendation orchestration service.

## Validation

- [ ] Validate required geometry fields.
- [ ] Validate positive dimensions.
- [ ] Validate valid dimension unit.
- [ ] Validate valid weight unit.
- [ ] Validate non-negative/positive weight according to use case.
- [ ] Validate positive quantity.
- [ ] Validate geometry type/source combinations.
- [ ] Validate optional part number uniqueness for saved parts.
- [ ] Validate duplicate bag/box codes.
- [ ] Add user-friendly validation messages.

---

# Phase 7 — Backend API

## API Foundation

- [ ] Add API route grouping under `/api`.
- [ ] Decide controllers vs minimal APIs and apply consistently.
- [ ] Add global exception handling.
- [ ] Add consistent API error response format.
- [x] Enable Swagger/OpenAPI in development.
- [ ] Add CORS policy for Angular client.
- [x] Add `/api/health` endpoint.

## Part Endpoints

- [ ] `GET /api/parts`
- [ ] `GET /api/parts/{id}`
- [ ] `POST /api/parts`
- [ ] `PUT /api/parts/{id}`
- [ ] `DELETE /api/parts/{id}` or soft delete

## Bag Endpoints

- [ ] `GET /api/bags`
- [ ] `GET /api/bags/{id}`
- [ ] `POST /api/bags`
- [ ] `PUT /api/bags/{id}`
- [ ] `DELETE /api/bags/{id}` or soft delete

## Box Endpoints

- [ ] `GET /api/boxes`
- [ ] `GET /api/boxes/{id}`
- [ ] `POST /api/boxes`
- [ ] `PUT /api/boxes/{id}`
- [ ] `DELETE /api/boxes/{id}` or soft delete

## Geometry Endpoints

- [ ] `POST /api/geometry/validate`
- [ ] Consider `POST /api/geometry/normalize` only if needed by client workflows.

## Recommendation Endpoints

- [ ] `POST /api/recommendations`
- [ ] `GET /api/recommendations/history`
- [ ] `GET /api/recommendations/{id}`
- [ ] Add integration tests for recommendation endpoint.
- [ ] Verify recommendation endpoint accepts new/unknown parts without part IDs.

---

# Phase 8 — Angular Frontend MVP

## App Foundation

- [ ] Replace Angular starter template.
- [ ] Confirm Angular routing setup.
- [ ] Add base layout.
- [ ] Add navigation menu.
- [ ] Add API environment configuration.
- [ ] Create shared API service.
- [ ] Create shared loading component.
- [ ] Create shared error component.
- [ ] Add typed frontend models for geometry, weight, units, and recommendations.

## Screens

- [ ] Dashboard/home screen.
- [ ] Part catalog list screen.
- [ ] Create/edit saved part form.
- [ ] Bag catalog list screen.
- [ ] Create/edit bag form.
- [ ] Box catalog list screen.
- [ ] Create/edit box form.
- [ ] Packaging recommendation form.
- [ ] Recommendation result screen.
- [ ] Recommendation history screen.

## Manual Regular-Part Input

- [ ] Length field.
- [ ] Width field.
- [ ] Height field.
- [ ] Dimension unit selector.
- [ ] Weight field.
- [ ] Weight unit selector.
- [ ] Quantity field.
- [ ] Rotation options.
- [ ] Stackability options.
- [ ] Clearance input.
- [ ] Clearance unit selector.
- [ ] Allow request without part number.
- [ ] Optional save-as-part action.

## Recommendation Result UX

- [ ] Show selected bag clearly.
- [ ] Show selected box clearly.
- [ ] Show total product weight.
- [ ] Show estimated gross package weight where available.
- [ ] Show box maximum weight.
- [ ] Show remaining weight capacity.
- [ ] Show space utilization percentage.
- [ ] Show weight utilization percentage.
- [ ] Show number of boxes required.
- [ ] Show recommendation explanation/reason codes.
- [ ] Show alternative bag and box options.
- [ ] Show form validation messages.
- [ ] Add empty states for catalogs.

---

# Phase 9 — Irregular Geometry Editor

## SVG Workspace

- [ ] Create SVG-based shape-editor component.
- [ ] Define engineering-coordinate to screen-coordinate transform.
- [ ] Ensure model coordinates are stored in selected engineering units, not pixels.
- [ ] Add zoom.
- [ ] Add pan.
- [ ] Add grid.
- [ ] Add grid snapping.
- [ ] Add point snapping.

## Shape Creation

- [ ] Click to create first vertex.
- [ ] Click to add next vertex.
- [ ] Automatically connect vertices with segments.
- [ ] Close shape action.
- [ ] Undo.
- [ ] Redo.
- [ ] Delete vertex.
- [ ] Move vertex.

## Orthogonal Mode

- [ ] Restrict segments to 0/90/180/270 degrees.
- [ ] Allow numeric segment length entry.
- [ ] Support direction-based creation.
- [ ] Test L shape.
- [ ] Test U shape.
- [ ] Test stepped shape.

## Free-Angle Mode

- [ ] Allow arbitrary angle.
- [ ] Allow numeric angle entry.
- [ ] Allow numeric segment length entry.
- [ ] Add optional angle snapping.
  - [ ] 15°
  - [ ] 30°
  - [ ] 45°
  - [ ] 90°
- [ ] Test V shape.
- [ ] Test W shape.
- [ ] Test triangular/angled shape.

## Irregular Part Metadata

- [ ] Thickness/depth field.
- [ ] Dimension unit selector.
- [ ] Weight field.
- [ ] Weight unit selector.
- [ ] Quantity field.
- [ ] Rotation constraints.
- [ ] Stackability constraints.

## Shape Validation UX

- [ ] Highlight self-intersections.
- [ ] Reject open shape on submission.
- [ ] Reject zero-area polygon.
- [ ] Show validation messages.
- [ ] Show normalized geometry preview.

---

# Phase 10 — Polygon Packing Engine

## Polygon Operations

- [ ] Evaluate/add a proven polygon geometry library.
- [ ] Implement polygon area calculation.
- [ ] Implement polygon translation.
- [ ] Implement polygon rotation.
- [ ] Implement polygon bounding box.
- [ ] Implement polygon intersection/collision.
- [ ] Implement polygon containment within box footprint.
- [ ] Implement polygon offset for clearance.
- [ ] Define geometric tolerance strategy.

## Irregular Placement

- [ ] Generate candidate orientations.
- [ ] Support 0/90/180/270 rotations.
- [ ] Support additional angles if allowed.
- [ ] Respect flip/mirror policy only when physically permitted.
- [ ] Generate candidate placement positions.
- [ ] Reject collisions.
- [ ] Reject boundary overflow.
- [ ] Score valid placements.

## Nesting

- [ ] Test whether concave shapes can use each other's empty regions.
- [ ] Add L-shape nesting scenarios.
- [ ] Add U-shape scenarios.
- [ ] Add V-shape scenarios.
- [ ] Add W-shape scenarios.
- [ ] Add arbitrary polygon scenarios.

## Weight Integration

- [ ] Apply total weight validation to irregular packing.
- [ ] Apply quantity splitting when weight capacity fails.
- [ ] Preserve weight utilization scoring.

## Polygon Packing Tests

- [ ] No-overlap tests.
- [ ] Boundary tests.
- [ ] Clearance tests.
- [ ] Rotation tests.
- [ ] Nesting tests.
- [ ] Mixed geometry/weight constraint tests.

---

# Phase 11 — Packaging Visualization

## 2D Visualization

- [ ] Define placement visualization data model.
- [ ] Generate top-view placement data.
- [ ] Generate side-view placement data.
- [ ] Render selected box boundary.
- [ ] Render regular part geometry.
- [ ] Render irregular polygon geometry.
- [ ] Render product position.
- [ ] Render orientation.
- [ ] Render quantity/layer information.
- [ ] Render clearance if useful.
- [ ] Display space utilization.
- [ ] Display weight utilization.
- [ ] Display total packaged weight.
- [ ] Display remaining weight capacity.

## Packing Instructions

- [ ] Generate operator-readable instructions from placement data.
- [ ] Add step/order labels if needed.
- [ ] Prepare export-ready instruction model.

## Future 3D Visualization

- [ ] Evaluate Three.js integration.
- [ ] Define 3D scene model.
- [ ] Render box and item geometry.
- [ ] Support rotation preview.
- [ ] Support layer visualization.

---

# Phase 12 — DXF and Structured Engineering Import

## File Upload

- [ ] Add file-upload endpoint.
- [ ] Add Angular upload UI.
- [ ] Validate file type and size.
- [ ] Define temporary file-retention rules.

## DXF Processing

- [ ] Evaluate DXF parser/library.
- [ ] Extract lines.
- [ ] Extract arcs.
- [ ] Extract polylines.
- [ ] Detect drawing units.
- [ ] Identify candidate outer profile.
- [ ] Convert to PackLogic geometry.
- [ ] Normalize geometry.
- [ ] Validate imported geometry.
- [ ] Extract mass/weight metadata where available.

## Import Confirmation

- [ ] Show imported geometry preview.
- [ ] Show detected units.
- [ ] Show detected weight if available.
- [ ] Allow user correction.
- [ ] Require confirmation before optimization.

---

# Phase 13 — Engineering Drawing Interpretation

## PDF/Image Drawing Support

- [ ] Add PDF engineering drawing support.
- [ ] Detect whether drawing is vector or raster where practical.
- [ ] Detect top view.
- [ ] Detect front view.
- [ ] Detect side view.
- [ ] Detect outer profiles.
- [ ] Detect overall dimensions.
- [ ] Detect dimension units.
- [ ] Detect weight/mass text where available.
- [ ] Ignore non-packaging details where possible.

## Geometry Proposal

- [ ] Generate proposed PackLogic geometry from drawing.
- [ ] Attach confidence score.
- [ ] Flag ambiguous geometry.
- [ ] Provide preview.
- [ ] Allow manual correction.
- [ ] Require user confirmation before optimization.

## Security

- [ ] Design local-processing path for sensitive drawings.
- [ ] Avoid mandatory third-party cloud upload.
- [ ] Define secure temporary-file handling.
- [ ] Define file deletion/retention behavior.

---

# Phase 14 — True 3D Geometry

## Mesh/CAD Model

- [ ] Create future `MeshGeometry` model.
- [ ] Define vertices and faces representation.
- [ ] Evaluate CAD B-Rep handling strategy.

## CAD Formats

- [ ] Add STEP support.
- [ ] Add STL support.
- [ ] Evaluate IGES support.
- [ ] Detect model units.
- [ ] Extract CAD mass properties where available.

## 3D Optimization

- [ ] Implement 3D bounding volume calculation.
- [ ] Implement 3D rotation/orientation handling.
- [ ] Implement 3D collision detection.
- [ ] Implement 3D box-boundary validation.
- [ ] Extend weight validation to 3D placements.
- [ ] Add true 3D packing heuristics.
- [ ] Add test fixtures for representative 3D parts.

## 3D Visualization

- [ ] Render mesh geometry.
- [ ] Render box volume.
- [ ] Render placements.
- [ ] Add orbit/zoom/pan controls.
- [ ] Add layer/exploded views.

---

# Phase 15 — Advanced Optimization

## Multi-Part Orders

- [ ] Support multiple different product geometries in one job.
- [ ] Calculate combined weight.
- [ ] Respect per-part orientation rules.
- [ ] Respect separation requirements.
- [ ] Determine whether parts can share packaging.

## Advanced Rules

- [ ] Fragility rules.
- [ ] Separation rules.
- [ ] Keep-upright rules.
- [ ] Do-not-flip rules.
- [ ] Do-not-place-on-face rules.
- [ ] Layer restrictions.

## Packaging Material Model

- [ ] Add packaging material weight.
- [ ] Add packaging material cost.
- [ ] Add foam/insert/divider models if needed.
- [ ] Include packaging materials in gross weight.

## Optimization Strategy

- [ ] Minimize wasted space.
- [ ] Minimize number of boxes.
- [ ] Balance weight utilization.
- [ ] Add packaging cost scoring.
- [ ] Add inventory-aware scoring.
- [ ] Add weight-distribution optimization.
- [ ] Benchmark optimization performance.
- [ ] Add timeout/iteration controls for complex searches.

---

# Phase 16 — Enterprise Features

## Security and Access

- [ ] Add authentication strategy.
- [ ] Add authorization roles:
  - [ ] Admin
  - [ ] Supervisor
  - [ ] Operator
- [ ] Protect admin catalog-management routes.
- [ ] Add audit fields to important entities.
- [ ] Add audit logs.

## Reporting

- [ ] Add recommendation history filters.
- [ ] Add packaging usage report.
- [ ] Add top-used bags report.
- [ ] Add top-used boxes report.
- [ ] Add wasted-space/efficiency report.
- [ ] Add weight-utilization report.
- [ ] Add overloaded-box prevention metrics where useful.
- [ ] Add PDF export for packaging instructions.

## Integrations

- [ ] Barcode scanning.
- [ ] ERP integration.
- [ ] PDM integration.
- [ ] CAD integration.
- [ ] Part/work-order lookup.
- [ ] Geometry retrieval by part number.
- [ ] Weight retrieval by part number.

## Multi-Site Support

- [ ] Facility configuration.
- [ ] Facility-specific bag inventory.
- [ ] Facility-specific box inventory.
- [ ] Facility-specific unit preferences.
- [ ] Facility-specific packaging rules.

---

# Phase 17 — Secure Enterprise Deployment

## Local Geometry Processor

- [ ] Design local PackLogic geometry processor/agent.
- [ ] Support local engineering-file parsing.
- [ ] Extract only required geometry/weight metadata.
- [ ] Send geometry-only payload when using central services.

## On-Premise Deployment

- [ ] Support on-premise API deployment.
- [ ] Support on-premise Angular hosting.
- [ ] Support on-premise SQL Server.
- [ ] Ensure engineering files remain inside customer network.

## Docker and Operations

- [ ] Add Docker support.
- [ ] Add Docker Compose for local/enterprise deployment.
- [ ] Add health checks.
- [ ] Add structured logging.
- [ ] Add environment-specific configuration.
- [ ] Add deployment guide.
- [ ] Add environment-variable documentation.

## File Security

- [ ] Define allowed engineering file types.
- [ ] Define file-size limits.
- [ ] Define configurable retention period.
- [ ] Support immediate deletion after geometry extraction.
- [ ] Ensure sensitive files are not logged.
- [ ] Document local-processing security model.

---

# Testing and Quality

## Backend Unit Tests

- [ ] Domain value objects.
- [ ] Unit conversions.
- [ ] Geometry validation.
- [ ] Geometry normalization.
- [ ] Cuboid orientation logic.
- [ ] Bag recommendation logic.
- [ ] Box recommendation logic.
- [ ] Weight validation.
- [ ] Quantity splitting.
- [ ] Space utilization scoring.
- [ ] Weight utilization scoring.
- [ ] Polygon operations.
- [ ] Irregular packing.

## Backend Integration Tests

- [ ] Part CRUD.
- [ ] Bag CRUD.
- [ ] Box CRUD.
- [ ] Geometry validation endpoint.
- [ ] Recommendation endpoint.
- [ ] Unknown-part recommendation.
- [ ] Recommendation history.

## Frontend Tests

- [ ] API services.
- [ ] Unit selectors.
- [ ] Manual dimension form validation.
- [ ] Weight form validation.
- [ ] Recommendation result rendering.
- [ ] Shape-editor geometry behavior.
- [ ] Empty states.
- [ ] Error states.

## CI/CD

- [ ] Add GitHub Actions workflow for backend build.
- [ ] Add GitHub Actions workflow for backend tests.
- [ ] Add GitHub Actions workflow for Angular build.
- [ ] Add frontend tests to CI.
- [ ] Add status badge to README after CI is configured.

---

# First Development Sprint Recommendation

The first focused sprint should build the geometry, measurement, and weight foundation before any recommendation logic.

## Sprint 1 Goal

Prepare PackLogic for geometry-driven and weight-aware backend development.

## Sprint 1 Tasks

- [ ] Document local development requirements.
- [ ] Finalize local SQL Server strategy.
- [ ] Align EF Core package versions.
- [ ] Confirm clean architecture references.
- [ ] Create `DimensionUnit` enum.
- [ ] Create `WeightUnit` enum.
- [ ] Create `GeometryType` enum.
- [ ] Create `GeometrySource` enum.
- [ ] Create `Dimensions` value object.
- [ ] Create `Weight` value object.
- [ ] Create unit conversion service.
- [ ] Create `Point2D` value object.
- [ ] Create `PartGeometry` model/abstraction.
- [ ] Create `CuboidGeometry`.
- [ ] Create initial `ExtrudedProfileGeometry` model.
- [ ] Create `Part` entity with optional part number.
- [ ] Add first unit tests for dimensions, weight, unit conversion, and geometry validation.

## Sprint 1 Deliverable

A clean domain foundation capable of representing a completely new regular or irregular extruded part, including geometry, dynamic units, weight, and packaging constraints, without requiring a predefined part catalog entry.

---

# MVP Completion Checklist

The initial regular-part MVP is considered complete when:

- [ ] User can enter a new product without a part number.
- [ ] User can optionally select a saved part.
- [ ] User can enter length, width, and height.
- [ ] User can choose a dimension unit.
- [ ] User can enter product weight.
- [ ] User can choose a weight unit.
- [ ] User can enter quantity.
- [ ] Units are normalized correctly.
- [ ] System recommends a bag when required.
- [ ] System recommends a box.
- [ ] Geometric fit is validated.
- [ ] Box weight capacity is validated.
- [ ] Quantity can be split when one box cannot satisfy geometry or weight constraints.
- [ ] System shows space utilization.
- [ ] System shows weight utilization.
- [ ] System explains why the recommendation was selected.
- [ ] Alternative bags and boxes are returned.
- [ ] Recommendation can be saved.
- [ ] User can view recommendation history.
- [ ] Angular frontend supports the complete workflow.
- [ ] Core recommendation logic has unit tests.
- [ ] Main API endpoints have integration tests.

The irregular-profile MVP expansion is considered complete when:

- [ ] User can define an irregular polygon profile.
- [ ] User can specify thickness/depth.
- [ ] User can specify weight and units.
- [ ] L-shaped parts can be represented and packed.
- [ ] U-shaped parts can be represented and packed.
- [ ] V-shaped parts can be represented and packed.
- [ ] W-shaped parts can be represented and packed.
- [ ] Polygon collision detection works.
- [ ] Polygon nesting can improve packing utilization.
- [ ] 2D arrangement visualization reflects actual placement data.

---

# Definition of Done

A task or feature is considered done when:

- [ ] It satisfies the intended operator workflow.
- [ ] It follows clean architecture boundaries.
- [ ] It has appropriate validation.
- [ ] It handles supported measurement units correctly.
- [ ] It handles weight constraints where applicable.
- [ ] It handles geometry constraints where applicable.
- [ ] It supports unknown parts where applicable.
- [ ] It has meaningful automated tests.
- [ ] It handles error and impossible-fit scenarios.
- [ ] It is documented where needed.
- [ ] `dotnet build` remains successful.
- [ ] Angular build remains successful.
- [ ] Existing tests remain passing.
