
use productdb;

INSERT INTO product_master 
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot, currency, is_trending, is_featured, created_at)
VALUES
(1, 'iPhone 14', 'https://example.com/images/iphone14.png', TRUE, 5, 120, 1, 101, 'Product', 'Electronics', 'INR', TRUE, TRUE, NOW()),
(2, 'Cargo Jeans', 'https://example.com/images/abc_jeans.png', TRUE, 4, 80, 1, 102, 'Product', 'Clothing', 'INR', FALSE, TRUE, NOW()),
(3, 'Yoga Mat', 'https://example.com/images/yoga_mat.png', TRUE, 4, 45, 1, 102, 'Product', 'Fitness', 'INR', FALSE, FALSE, NOW());

INSERT INTO product_variant_master 
(id, product_id, name, price, mrp, stock_quantity, is_default)
VALUES
(1, 1, 'iPhone 14 - 128GB', 79900.00, 84900.00, 50, TRUE),
(2, 1, 'iPhone 14 - 256GB', 89900.00, 94900.00, 30, FALSE),
(3, 2, 'Cargo Jeans - Size M', 1999.00, 2499.00, 100, TRUE),
(4, 2, 'Cargo Jeans - Size L', 1999.00, 2499.00, 80, FALSE);
-- Note: Yoga Mat has no variants

INSERT INTO product_attribute_value 
(id, product_id, product_variant_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, value, created_at)
VALUES
-- iPhone Variants
(1, NULL, 1, 1, 1, 'Color', 'Black', NOW()),
(2, NULL, 2, 1, 2, 'Color', 'White', NOW()),

-- Jeans Variants
(3, NULL, 3, 2, 3, 'Fit', 'Slim', NOW()),
(4, NULL, 4, 2, 4, 'Fit', 'Regular', NOW()),

-- Yoga Mat (no variant → product-level)
(5, 3, NULL, 3, 5, 'Color', 'Blue', NOW()),
(6, 3, NULL, 4, 6, 'Material', 'Eco-friendly', NOW());

INSERT INTO product_image 
(id, product_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'image', 'https://example.com/images/iphone14_front.png', 'iPhone 14 Front', 1, NOW()),
(2, 1, 'image', 'https://example.com/images/iphone14_back.png', 'iPhone 14 Back', 2, NOW()),
(3, 2, 'image', 'https://example.com/images/abc_jeans_front.png', 'Cargo Jeans Front', 1, NOW()),
(4, 2, 'image', 'https://example.com/images/abc_jeans_back.png', 'Cargo Jeans Back', 2, NOW()),
(5, 3, 'image', 'https://example.com/images/yoga_mat.png', 'Yoga Mat', 1, NOW());

INSERT INTO product_variant_image 
(id, product_variant_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'image', 'https://example.com/images/iphone14_128_black.png', 'iPhone 14 128GB Black', 1, NOW()),
(2, 2, 'image', 'https://example.com/images/iphone14_256_white.png', 'iPhone 14 256GB White', 1, NOW());


INSERT INTO product_addon 
(id, product_id, product_variant_id, name, description, price, currency, is_active, display_order, created_at)
VALUES
-- Variant-level addons (apply to specific variants)
(1, NULL, 1, 'Screen Protector', 'Tempered Glass Screen Protector', 499.00, 'INR', TRUE, 1, NOW()),
(2, NULL, 2, 'Fast Charger', '20W USB-C Power Adapter', 1499.00, 'INR', TRUE, 2, NOW()),

-- Product-level addons (apply to whole product)
(3, 2, NULL, 'Extra Button', 'Spare Jeans Button', 50.00, 'INR', TRUE, 1, NOW()),

-- No variant product (Yoga Mat)
(4, 3, NULL, 'Carry Bag', 'Yoga Mat Carry Bag', 250.00, 'INR', TRUE, 1, NOW());
