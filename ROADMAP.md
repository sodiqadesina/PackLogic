# PackLogic Roadmap

PackLogic is a geometry-driven, weight-aware manufacturing packaging optimization platform designed to help operators, production teams, warehouse teams, and shipping teams quickly determine the best plastic bag, shipping box, and packing arrangement for regular, irregular, and eventually full 3D parts.

This roadmap defines the professional delivery path from the current scaffolded repository to a working MVP and then to a production-ready platform aligned with the architecture and goals described in `README.md`.

---

## 1. Product Vision

Build a reliable digital packaging assistant that turns packaging decisions into a repeatable, geometry-driven, weight-aware workflow.

The system should help users answer:

> Given a product's geometry, weight, quantity, packaging requirements, and the available bag and box inventory, what packaging should be used and how should the product be arranged while remaining within both space and weight limits?

The product does not need to be known to PackLogic in advance. Users should be able to package both saved catalog parts and completely new parts.

---

## 2. Guiding Principles

- **Geometry-first design:** Packing decisions should be based on geometry rather than requiring predefined part records.
- **Weight-aware optimization:** A package is valid only when it satisfies both geometric fit and weight-capacity constraints.
- **Dynamic units:** Users can work in their preferred dimension and weight units while PackLogic normalizes internally for calculation.
- **Operator-first design:** The UI should be fast, simple, and usable on the shop floor.
- **Unknown-part support:** A part number is useful for reuse, but not required to obtain a recommendation.
- **Multiple geometry inputs:** Manual dimensions, shape editing, engineering drawings, and CAD integrations should all converge into a common internal geometry model.
- **Explainable recommendations:** Every recommendation should explain why it was selected.
- **Deterministic early logic:** Initial algorithms should be predictable and testable before introducing advanced heuristics.
- **Clean architecture:** Domain rules should remain independent from UI, database, and infrastructure concerns.
- **Incremental delivery:** Build a useful end-to-end flow first, then expand to irregular geometry, CAD input, 3D packing, and enterprise integration.
- **Data quality first:** Recommendations are only as reliable as the geometry, weight, packaging constraints, and packaging inventory supplied to the system.
- **Security-aware engineering integration:** Sensitive drawings and CAD files should support local processing and on-premise deployment options.

---

## 3. Current Repository Baseline

The repository currently contains the foundation for a full-stack application:

- ASP.NET Core Web API project
- Domain, Application, Infrastructure, and Optimization projects
- Unit test and integration test projects
- Angular client project
- Swagger configuration
- Health-check endpoint
- Layer-specific dependency injection registration
- Root README describing the current geometry-driven and weight-aware product architecture

The next step is to build the real geometry, measurement, weight, domain, optimization, persistence, API, and Angular workflow layers on top of this foundation.

---

## 4. Target Product Model

PackLogic should model a packaging request using more than only length, width, and height.

A product may contain:

- Geometry
- Geometry type
- Geometry source
- Dimension unit
- Weight
- Weight unit
- Quantity
- Rotation constraints
- Stackability rules
- Clearance requirements
- Packaging restrictions

Supported geometry levels should evolve through the roadmap:

1. **Cuboid geometry** for regular products.
2. **Extruded-profile geometry** for irregular 2D profiles with thickness/depth.
3. **True 3D geometry** for CAD and mesh-based products.

Possible geometry sources include:

- Manual dimensions
- CAD-like shape editor
- Engineering drawing
- DXF
- STEP
- Saved part geometry
- CAD/PDM/ERP integration

All geometry sources should ultimately produce a common internal representation for the packing engine.

---

## 5. Measurement and Unit Strategy

Users should be able to choose their preferred units.

### Dimension Units

Initial support should include:

- mm
- cm
- m
- in
- ft

### Weight Units

Initial support should include:

- mg
- g
- kg
- oz
- lb

PackLogic should normalize values internally before geometry and weight calculations.

Recommended internal standards:

- Dimensions normalized to millimetres
- Weight normalized to kilograms

The UI should continue to display values using the unit selected by the user.

---

## 6. Target Architecture

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
    | Use cases, validation, orchestration
    v
PackLogic.Domain
    |
    +------------------------------+
    |                              |
    v                              v
Geometry + Measurement       Packaging Rules
Weight Model
    |
    v
PackLogic.Optimization
    |
    +------------------------------+
    |                              |
    v                              v
Geometry Processing         Packing Algorithms
Unit Conversion             Weight Validation
    |                              |
    +---------------+--------------+
                    |
                    v
          Packaging Recommendation
                    |
                    v
PackLogic.Infrastructure
                    |
                    | EF Core
                    v
               SQL Server
```

### Project Responsibilities

| Project | Responsibility |
| --- | --- |
| `PackLogic.Api` | HTTP endpoints, request/response contracts, API configuration, Swagger/OpenAPI |
| `PackLogic.Application` | Use cases, DTOs, validation, orchestration, service interfaces |
| `PackLogic.Domain` | Core entities, geometry models, value objects, enums, business rules |
| `PackLogic.Infrastructure` | EF Core, SQL Server, repositories, persistence, external-system adapters |
| `PackLogic.Optimization` | Geometry operations, unit conversion, weight validation, bag/box recommendation algorithms, packing logic |
| `PackLogic.Client` | Angular UI, forms, geometry input, packaging workflow, visualization |
| `tests` | Unit tests, integration tests, regression coverage |

---

# 7. Delivery Phases

## Phase 1 — Project Foundation

**Goal:** Complete the development foundation before implementing domain logic.

Deliverables:

- Repository setup
- .NET solution structure
- Angular workspace
- Clean architecture project separation
- Layer-specific dependency injection
- Swagger/OpenAPI configuration
- Health-check endpoint
- Root development setup instructions
- Local development requirements
- Final local database strategy
- Consistent naming conventions

Exit Criteria:

- `dotnet build` succeeds
- Angular client builds and starts locally
- Swagger is available in development
- `/api/health` responds successfully
- Local setup requirements are documented
- Database strategy is selected

---

## Phase 2 — Geometry, Measurement, Weight, and Domain Foundation

**Goal:** Build the core domain model that every future recommendation depends on.

Deliverables:

- `Part` entity
- `PartGeometry` abstraction/model
- `GeometryType` enum
- `GeometrySource` enum
- `CuboidGeometry`
- `ExtrudedProfileGeometry`
- `Point2D`
- `LineSegment`
- `ArcSegment`
- `Dimensions` value object
- `Weight` value object
- `DimensionUnit` enum
- `WeightUnit` enum
- Unit conversion service
- Rotation rules
- Stackability rules
- Clearance rules
- Geometry validation rules
- Geometry normalization rules
- Unit tests for geometry and measurement models

Exit Criteria:

- Regular and irregular extruded products can be represented without requiring a predefined part number
- Dimension and weight units can be converted reliably
- Invalid geometry and invalid measurements are rejected
- Domain model remains independent of EF Core and Angular

---

## Phase 3 — Data and Persistence

**Goal:** Persist parts, geometry, packaging inventory, and recommendation history.

Deliverables:

- EF Core packages aligned with the selected .NET version
- `PackLogicDbContext`
- SQL Server connection configuration
- Part persistence
- Geometry persistence
- Geometry versioning
- Weight persistence
- Bag persistence
- Box persistence
- Box maximum-weight persistence
- Packaging job persistence
- Recommendation persistence
- Fluent API mappings
- Initial migration
- Development seed data

Exit Criteria:

- Database can be created from migrations
- Parts can store reusable geometry and weight
- Geometry versions are traceable
- Bags and boxes store units and capacity information
- Sample packaging inventory is available for testing

---

## Phase 4 — Regular Packing Engine

**Goal:** Build the first geometry-aware, weight-aware packing calculations for regular cuboid products.

Deliverables:

- Cuboid fit calculation
- Orientation generation
- Rotation-policy enforcement
- Clearance handling
- Quantity handling
- Total product weight calculation
- Box maximum-weight validation
- Box-boundary validation
- Space-utilization scoring
- Weight-utilization scoring
- Quantity splitting when weight or geometry prevents a single-box solution
- Unit tests

Exit Criteria:

- A new or saved cuboid product can be evaluated against available boxes
- A box is rejected if either geometry or weight fails
- Valid orientations are tested deterministically
- Quantity may be split across boxes when required

---

## Phase 5 — Bag Optimization

**Goal:** Determine the most appropriate bag before boxing when bagging is required.

Deliverables:

- Bag fit calculation
- Clearance allowance
- Rotation support
- Dynamic unit support
- Bag utilization scoring
- Alternative bag recommendations
- Recommendation reason codes
- Unit tests

Exit Criteria:

- PackLogic can recommend the best valid bag for a regular part
- Alternative bags are returned
- Selected units do not affect calculation correctness
- Recommendation explains why the bag was chosen

---

## Phase 6 — Box Optimization

**Goal:** Select the best box using effective packaged geometry and weight constraints.

Deliverables:

- Effective packaged geometry calculation
- Box fit calculation
- Maximum-weight validation
- Total product weight calculation
- Estimated gross packaged weight model
- Quantity splitting by geometry
- Quantity splitting by weight
- Dynamic unit support
- Space-utilization ranking
- Weight-utilization ranking
- Alternative box recommendations
- Recommendation explanations
- Unit tests

Exit Criteria:

- A box is recommended only when geometric and weight constraints both pass
- PackLogic can return multiple valid alternatives
- Weight-limited and geometry-limited scenarios are handled clearly

---

## Phase 7 — Backend API MVP

**Goal:** Expose the domain and recommendation workflow through clean REST endpoints.

Deliverables:

- Part endpoints
- Bag endpoints
- Box endpoints
- Geometry validation endpoint
- Packaging recommendation endpoint
- Recommendation history endpoints
- Typed DTOs
- Request validation
- Consistent API error responses
- Swagger/OpenAPI documentation
- CORS configuration for Angular
- Integration tests

Suggested endpoints:

```text
GET    /api/parts
POST   /api/parts
GET    /api/parts/{id}
PUT    /api/parts/{id}
DELETE /api/parts/{id}

GET    /api/bags
POST   /api/bags
GET    /api/bags/{id}
PUT    /api/bags/{id}
DELETE /api/bags/{id}

GET    /api/boxes
POST   /api/boxes
GET    /api/boxes/{id}
PUT    /api/boxes/{id}
DELETE /api/boxes/{id}

POST   /api/geometry/validate
POST   /api/recommendations
GET    /api/recommendations/history
GET    /api/recommendations/{id}
```

Exit Criteria:

- The API can support the full regular-part recommendation workflow
- API contracts include geometry, weight, units, quantity, and constraints
- Swagger documents all main request/response structures

---

## Phase 8 — Angular Packaging Application

**Goal:** Build the first complete operator-facing workflow.

Deliverables:

- Replace Angular starter content
- Routing and navigation
- Dashboard/home screen
- Part catalog screen
- Bag catalog screen
- Box catalog screen
- Packaging job workflow
- Manual dimension entry
- Selectable dimension unit
- Product weight input
- Selectable weight unit
- Quantity input
- Packaging constraints
- Recommendation results
- Alternative recommendations
- Space utilization display
- Weight utilization display
- Loading states
- Error states

Exit Criteria:

- User can enter a completely new regular part and receive a recommendation
- User can also select a previously saved part
- Unit selectors work correctly
- Recommendation result clearly shows geometry fit and weight fit

---

## Phase 9 — Irregular Geometry Editor

**Goal:** Support manually defined irregular profiles without requiring full CAD software.

Deliverables:

- SVG-based geometry workspace
- Click-to-create vertices
- Line tool
- Orthogonal mode
- Free-angle mode
- Numeric segment lengths
- Numeric angle input
- Selectable dimension units
- Grid display
- Snapping
- Undo/redo
- Close-shape action
- Vertex editing
- Thickness/depth input
- Weight input
- Selectable weight unit
- Shape validation
- Geometry preview

Exit Criteria:

- Users can define valid L, U, V, W, stepped, angled, and arbitrary polygon profiles
- Editor stores real engineering coordinates rather than pixels
- Geometry can be submitted to the same backend geometry model used by other sources

---

## Phase 10 — Polygon Packing Engine

**Goal:** Optimize irregular extruded profiles using polygon-aware placement logic.

Deliverables:

- Polygon area calculation
- Polygon rotation
- Polygon translation
- Collision detection
- Box-boundary detection
- Clearance offsets
- Irregular-shape nesting
- Orientation search
- Weight-capacity validation
- Placement scoring
- Tests for L-shaped parts
- Tests for U-shaped parts
- Tests for V-shaped parts
- Tests for W-shaped parts
- Tests for arbitrary polygons

Exit Criteria:

- Irregular parts are packed using actual profiles rather than only bounding rectangles
- Valid rotations and nesting arrangements can improve utilization
- Weight rules remain enforced throughout irregular packing

---

## Phase 11 — Packaging Visualization

**Goal:** Generate clear visual instructions for operators.

Deliverables:

- Top-view placement data
- Side-view placement data
- Product geometry rendering
- Box boundary rendering
- Orientation display
- Quantity display
- Layer display
- Clearance display
- Space utilization display
- Weight utilization display
- Total package weight display
- Remaining box weight capacity
- Packing instructions

Exit Criteria:

- Operator can visually understand how the recommended arrangement should be packed
- Visualization reflects the actual optimization output

---

## Phase 12 — DXF and Engineering File Import

**Goal:** Accept structured engineering geometry without requiring manual redraw.

Deliverables:

- File-upload workflow
- DXF parsing
- Line extraction
- Arc extraction
- Polyline extraction
- Unit detection
- Conversion into PackLogic geometry
- Mass/weight metadata extraction where available
- Geometry preview
- User confirmation workflow

Exit Criteria:

- A supported DXF can be converted into PackLogic geometry
- Imported geometry can flow through the existing packing engine

---

## Phase 13 — Engineering Drawing Interpretation

**Goal:** Extract useful packaging geometry from engineering drawings.

Deliverables:

- PDF drawing support
- View detection
- Outer-profile detection
- Overall-dimension detection
- Unit detection
- Product weight/mass detection where available
- Relevant geometry extraction
- Confidence scoring
- User correction workflow
- Local-processing option for confidential drawings

Exit Criteria:

- PackLogic can produce a reviewable geometry proposal from supported drawings
- Users can confirm or correct extracted geometry before optimization
- Sensitive drawings do not require third-party cloud processing

---

## Phase 14 — True 3D Geometry

**Goal:** Support products that cannot be represented by a single extruded 2D profile.

Deliverables:

- Mesh geometry model
- STEP support
- STL support
- IGES evaluation
- 3D collision detection
- 3D orientation handling
- CAD mass-property extraction where available
- True 3D packing
- 3D arrangement visualization

Exit Criteria:

- PackLogic can evaluate complex 3D parts using real 3D geometry
- 3D collision and placement logic are validated by tests

---

## Phase 15 — Advanced Optimization

**Goal:** Improve recommendation quality for complex manufacturing scenarios.

Deliverables:

- Multi-part order support
- Multi-layer packing
- Advanced nesting heuristics
- Fragility rules
- Separation rules
- Packaging material weight calculation
- Packaging material scoring
- Packaging cost scoring
- Inventory-aware recommendations
- Weight-distribution optimization
- Performance optimization

Exit Criteria:

- The engine can balance geometry, weight, cost, packaging inventory, and handling constraints across complex orders

---

## Phase 16 — Enterprise Features

**Goal:** Prepare PackLogic for controlled organizational deployment.

Deliverables:

- Authentication
- Authorization
- Admin role
- Supervisor role
- Operator role
- Audit logs
- Reporting dashboard
- PDF packaging instructions
- Barcode scanning
- ERP integration
- PDM integration
- CAD integration
- Multi-location support
- Facility-level unit preferences

Exit Criteria:

- Organizations can manage users, packaging data, audit history, and integrations securely

---

## Phase 17 — Secure Enterprise Deployment

**Goal:** Support sensitive engineering environments and enterprise deployment models.

Deliverables:

- Local geometry processor
- Geometry-and-weight-only cloud transfer
- On-premise deployment
- Docker deployment
- Secure engineering file handling
- Configurable file-retention policies
- Enterprise deployment documentation

Exit Criteria:

- Customers can process confidential engineering files without sending originals to third-party cloud services
- PackLogic can be deployed inside controlled enterprise networks

---

## 8. Suggested Milestone Plan

| Milestone | Focus | Outcome |
| --- | --- | --- |
| M1 | Foundation | Repo builds, local setup documented |
| M2 | Geometry/Domain | Geometry, weight, units, and rules modeled |
| M3 | Persistence | SQL Server and EF Core store packaging data |
| M4 | Regular Engine | Cuboid packing and weight validation work |
| M5 | Bag/Box Optimization | Full regular-part recommendation works |
| M6 | API MVP | Backend exposes complete recommendation workflow |
| M7 | Angular MVP | User can complete workflow from browser |
| M8 | Irregular Editor | User can define irregular profiles |
| M9 | Polygon Packing | Irregular nesting and collision logic work |
| M10 | Visualization | Operator-facing arrangement instructions available |
| M11 | DXF Import | Structured engineering geometry can be imported |
| M12 | Drawing Interpretation | PDF engineering drawings can propose geometry |
| M13 | True 3D | CAD/mesh-based packing becomes possible |
| M14 | Advanced Optimization | Multi-part and smarter packing supported |
| M15 | Enterprise | Auth, reporting, integrations, controlled rollout |
| M16 | Secure Deployment | Local/on-premise engineering workflows supported |

---

## 9. MVP Scope

The first useful MVP should support:

1. User enters a new regular part or selects a saved part.
2. User enters dimensions and selects a dimension unit.
3. User enters weight and selects a weight unit.
4. User enters quantity and packaging constraints.
5. PackLogic normalizes units.
6. PackLogic recommends a bag when required.
7. PackLogic calculates effective packaged geometry.
8. PackLogic calculates total product weight.
9. PackLogic recommends a box.
10. PackLogic validates both space and weight requirements.
11. PackLogic returns alternatives and explanation reasons.
12. User can save the result for future reuse.

### MVP In Scope

- Regular cuboid geometry
- Dynamic dimension units
- Dynamic weight units
- Product weight
- Quantity
- Part catalog
- Bag catalog
- Box catalog
- Bag recommendation
- Box recommendation
- Geometric fit validation
- Weight-capacity validation
- Space utilization
- Weight utilization
- Rotation rules
- Clearance rules
- Recommendation history
- REST API
- Angular workflow
- Unit and integration tests

### MVP Expansion Immediately After Regular Flow

- Irregular extruded-profile geometry
- CAD-like shape editor
- Polygon collision
- Polygon nesting
- 2D arrangement visualization

### Later Scope

- DXF import
- Engineering PDF interpretation
- STEP/STL/IGES support
- True 3D packing
- ERP/PDM/CAD integrations
- Barcode scanning
- Authentication and enterprise deployment

---

## 10. Technical Standards

### Backend Standards

- Use clean architecture boundaries.
- Keep domain entities free of infrastructure dependencies.
- Keep optimization logic deterministic and testable.
- Use typed DTOs for API contracts.
- Validate geometry, weight, units, and quantity before optimization.
- Normalize units in one consistent service.
- Keep geometry-source logic separate from packing logic.
- Do not make part number mandatory for packaging requests.
- Use structured error responses.
- Add tests for geometry and weight edge cases.

### Frontend Standards

- Use Angular standalone components where appropriate.
- Keep API access inside services.
- Use typed request and response models.
- Keep forms validated and operator-friendly.
- Allow users to choose dimension and weight units.
- Store geometry using engineering coordinates rather than display pixels.
- Display recommendation explanations, not only results.
- Design for speed and clarity on production floors.

### Data Standards

- Store or normalize dimensions consistently.
- Store or normalize weight consistently.
- Preserve the user's selected unit where useful for display/audit purposes.
- Avoid hard-coded packaging sizes in business logic.
- Treat bags and boxes as configurable catalog data.
- Version reusable part geometry.
- Track recommendation history for auditability.

### Security Standards

- Treat engineering drawings and CAD files as potentially confidential intellectual property.
- Avoid requiring cloud upload of original engineering files.
- Support local geometry extraction and on-premise options in later phases.
- Store only the minimum engineering data required for packaging where practical.

---

## 11. Definition of Done

A feature is considered done when:

- It satisfies the intended operator workflow.
- It has appropriate validation.
- It supports required unit conversions.
- It handles geometry and weight constraints correctly where applicable.
- It has meaningful automated test coverage.
- It handles error and impossible-fit cases.
- It is documented where needed.
- It does not break existing build or tests.
- It follows the repository architecture.
- It remains compatible with unknown-part workflows.

---

## 12. Long-Term Product Direction

The long-term goal is to turn PackLogic into a production-ready packaging optimization platform that can understand product geometry and weight directly from manufacturing and engineering systems, then automatically produce efficient, safe, repeatable packaging decisions.

The ideal future workflow is:

```text
Scan Part / Select Work Order
            |
            v
Retrieve Approved Geometry + Weight
            |
            v
Apply Packaging Requirements
            |
            v
Optimize Bagging
            |
            v
Optimize Boxing
            |
            v
Validate Geometry + Weight
            |
            v
Generate Packing Arrangement
            |
            v
Provide Visual Operator Instructions
            |
            v
Save Packaging Result
```

PackLogic ultimately aims to reduce packaging decision time, reduce material waste, improve box utilization, prevent overloaded packaging, standardize packaging decisions, protect sensitive engineering information, and integrate packaging optimization into existing manufacturing workflows.
