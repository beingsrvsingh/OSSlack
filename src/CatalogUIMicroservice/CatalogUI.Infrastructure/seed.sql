use cataloguidb;
select * from layouts;

-- Shop by Category
INSERT INTO layouts 
(id, page_name, layout_json, tenant_id, user_role, created_at, updated_at, is_active, version, created_by, updated_by, description)
VALUES
(1, 'home', 
 '{"type": "section", "title": "Shop by Category", "display": {"style": "card", "columns": 3}, "component": "category_grid", "dataEndpoint": "/v1/Category/all-categories", "microservice": "catalog"}',
 'tenant_001', 'customer', '2025-09-04 21:04:54', NULL, 1, 1, 'system', NULL, 'Layout for category grid section'),


-- Trending Products

(2, 'home', 
 '{"type": "grid", "title": "Trending Products", "display": {"autoplay": true, "cardStyle": "compact"}, "component": "product_carousel", "dataEndpoint": "/v1/Category/all-categories", "microservice": "catalog"}',
 'tenant_002', 'customer', '2025-09-04 21:06:29', NULL, 1, 1, 'system', NULL, 'Carousel layout for trending products'),


-- Featured Banners

(3, 'home', 
 '{"type": "horizontal", "title": "Featured", "display": {"autoplay": true, "interval": 5, "fullWidth": true}, "component": "banner_slider", "dataEndpoint": "/v1/Category/all-categories", "microservice": "catalog"}',
 'tenant_003', 'guest', '2025-09-04 21:06:29', NULL, 1, 1, 'system', NULL, 'Slider for hero banners on homepage'),


-- New Arrivals

(4, 'home', 
 '{"type": "grid", "title": "New Arrivals", "display": {"style": "modern", "columns": 2}, "component": "product_grid", "dataEndpoint": "/v1/Category/all-categories", "microservice": "catalog"}',
 'tenant_004', 'customer', '2025-09-04 21:06:29', NULL, 1, 1, 'system', NULL, 'Product grid layout for new arrivals'),


-- Recommended For You

(5, 'home', 
 '{"type": "section", "title": "Recommended For You", "display": {"style": "list", "showRatings": true}, "component": "product_list", "dataEndpoint": "/v1/Category/all-categories", "microservice": "catalog"}',
 'tenant_005', 'premium_user', '2025-09-04 21:06:29', NULL, 1, 1, 'system', NULL, 'Role-specific recommendation list');


SELECT * FROM layouts
WHERE JSON_EXTRACT(layout_json, '$.component') = 'product_grid';

