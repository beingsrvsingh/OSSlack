use cataloguidb;
select * from layouts;
-- Shop by Category
INSERT INTO `layouts` (
    `page_name`, `layout_json`, `tenant_id`, `user_role`,
    `created_at`, `updated_at`, `is_active`, `version`,
    `created_by`, `updated_by`, `description`
)
VALUES (
    'home',
    JSON_OBJECT(
        'type', 'section',
        'title', 'Shop by Category',
        'component', 'category_grid',
        'dataEndpoint', '/api/categories',
        'display', JSON_OBJECT(
            'columns', 3,
            'style', 'card'
        )
    ),
    'tenant_001',
    'customer',
    NOW(),
    NULL,
    TRUE,
    1,
    'system',
    NULL,
    'Layout for category grid section'
);

-- Trending Products

INSERT INTO `layouts` (
    `page_name`, `layout_json`, `tenant_id`, `user_role`,
    `created_at`, `updated_at`, `is_active`, `version`,
    `created_by`, `updated_by`, `description`
)
VALUES (
    'home',
    JSON_OBJECT(
        'type', 'grid',
        'title', 'Trending Products',
        'component', 'product_carousel',
        'dataEndpoint', '/api/products/trending',
        'display', JSON_OBJECT(
            'autoplay', true,
            'cardStyle', 'compact'
        )
    ),
    'tenant_002',
    'customer',
    NOW(),
    NULL,
    TRUE,
    1,
    'system',
    NULL,
    'Carousel layout for trending products'
);

-- Featured Banners

INSERT INTO `layouts` (
    `page_name`, `layout_json`, `tenant_id`, `user_role`,
    `created_at`, `updated_at`, `is_active`, `version`,
    `created_by`, `updated_by`, `description`
)
VALUES (
    'home',
    JSON_OBJECT(
        'type', 'horizontal',
        'title', 'Featured',
        'component', 'banner_slider',
        'dataEndpoint', '/api/banners/featured',
        'display', JSON_OBJECT(
            'autoplay', true,
            'interval', 5,
            'fullWidth', true
        )
    ),
    'tenant_003',
    'guest',
    NOW(),
    NULL,
    TRUE,
    1,
    'system',
    NULL,
    'Slider for hero banners on homepage'
);

-- New Arrivals

INSERT INTO `layouts` (
    `page_name`, `layout_json`, `tenant_id`, `user_role`,
    `created_at`, `updated_at`, `is_active`, `version`,
    `created_by`, `updated_by`, `description`
)
VALUES (
    'home',
    JSON_OBJECT(
        'type', 'grid',
        'title', 'New Arrivals',
        'component', 'product_grid',
        'dataEndpoint', '/api/products/new',
        'display', JSON_OBJECT(
            'columns', 2,
            'style', 'modern'
        )
    ),
    'tenant_004',
    'customer',
    NOW(),
    NULL,
    TRUE,
    1,
    'system',
    NULL,
    'Product grid layout for new arrivals'
);

-- Recommended For You

INSERT INTO `layouts` (
    `page_name`, `layout_json`, `tenant_id`, `user_role`,
    `created_at`, `updated_at`, `is_active`, `version`,
    `created_by`, `updated_by`, `description`
)
VALUES (
    'home',
    JSON_OBJECT(
        'type', 'section',
        'title', 'Recommended For You',
        'component', 'product_list',
        'dataEndpoint', '/api/products/recommended',
        'display', JSON_OBJECT(
            'style', 'list',
            'showRatings', true
        )
    ),
    'tenant_005',
    'premium_user',
    NOW(),
    NULL,
    TRUE,
    1,
    'system',
    NULL,
    'Role-specific recommendation list'
);

SELECT * FROM layouts
WHERE JSON_EXTRACT(layout_json, '$.component') = 'product_grid';

