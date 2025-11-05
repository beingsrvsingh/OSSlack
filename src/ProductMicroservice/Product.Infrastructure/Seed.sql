
use productdb;
-- ----------------------------
-- Product Master
-- ----------------------------
INSERT INTO product_master
(id, name, currency, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot, created_at, updated_at)
VALUES
(1, 'iPhone 14', 'INR', 'https://example.com/images/iphone14-thumb.jpg', 1, 4.9, 1200, 1, 1, 'Electronics', 'Mobile', NOW(), NOW()),
(2, 'iPhone 14 Pro', 'INR', 'https://example.com/images/iphone14pro-thumb.jpg', 1, 4.8, 900, 1, 1, 'Electronics', 'Mobile', NOW(), NOW()),
(3, 'iPhone 14 Pro Max', 'INR', 'https://example.com/images/iphone14promax-thumb.jpg', 1, 4.7, 700, 1, 1, 'Electronics', 'Mobile', NOW(), NOW());

-- ----------------------------
-- Product Variant Master
-- ----------------------------
INSERT INTO product_variant_master
(id, product_master_id, name, price, mrp, stock_quantity, is_default)
VALUES
-- iPhone 14 Variants
(101, 1, 'iPhone 14 128GB Silver', 999, 1099, 50, 1),
(102, 1, 'iPhone 14 128GB Black', 999, 1099, 40, 0),
(103, 1, 'iPhone 14 256GB Silver', 1099, 1199, 30, 0),
-- iPhone 14 Pro Variants
(201, 2, 'iPhone 14 Pro 128GB Silver', 1199, 1299, 50, 1),
(202, 2, 'iPhone 14 Pro 128GB Graphite', 1199, 1299, 40, 0),
(203, 2, 'iPhone 14 Pro 256GB Silver', 1299, 1399, 20, 0),
-- iPhone 14 Pro Max Variants
(301, 3, 'iPhone 14 Pro Max 128GB Silver', 1299, 1399, 50, 1),
(302, 3, 'iPhone 14 Pro Max 256GB Graphite', 1399, 1499, 30, 0);

-- ----------------------------
-- Product Variant Images
-- ----------------------------
INSERT INTO product_variant_image (id, product_variant_id, image_url, sort_order, alt_text)
VALUES
(1, 101, 'https://example.com/images/iphone14-128gb-silver.jpg', 1, 'iPhone 14 128GB Silver front'),
(2, 102, 'https://example.com/images/iphone14-128gb-black.jpg', 1, 'iPhone 14 128GB Black front'),
(3, 103, 'https://example.com/images/iphone14-256gb-silver.jpg', 1, 'iPhone 14 256GB Silver front'),
(4, 201, 'https://example.com/images/iphone14-pro-128gb-silver.jpg', 1, 'iPhone 14 Pro 128GB Silver front'),
(5, 202, 'https://example.com/images/iphone14-pro-128gb-graphite.jpg', 1, 'iPhone 14 Pro 128GB Graphite front'),
(6, 203, 'https://example.com/images/iphone14-pro-256gb-silver.jpg', 1, 'iPhone 14 Pro 256GB Silver front'),
(7, 301, 'https://example.com/images/iphone14-pro-max-128gb-silver.jpg', 1, 'iPhone 14 Pro Max 128GB Silver front'),
(8, 302, 'https://example.com/images/iphone14-pro-max-256gb-graphite.jpg', 1, 'iPhone 14 Pro Max 256GB Graphite front');

-- ----------------------------
-- Product Images
-- ----------------------------
INSERT INTO product_image
(id, product_id, image_url, sort_order, alt_text)
VALUES
(1, 1, 'https://example.com/images/iphone14-1.jpg', 1, 'iPhone 14 Front'),
(2, 1, 'https://example.com/images/iphone14-2.jpg', 2, 'iPhone 14 Back'),
(3, 2, 'https://example.com/images/iphone14pro-1.jpg', 1, 'iPhone 14 Pro Front'),
(4, 3, 'https://example.com/images/iphone14promax-1.jpg', 1, 'iPhone 14 Pro Max Front');

-- ----------------------------
-- Product Attribute Value (Master Level)
-- ----------------------------
INSERT INTO product_attribute_value
(id, product_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, attribute_label, value, created_at)
VALUES
(1, 1, 1, 1, 'brand', 'Brand', 'Apple', NOW()),
(2, 2, 1, 1, 'brand', 'Brand', 'Apple', NOW()),
(3, 3, 1, 1, 'brand', 'Brand', 'Apple', NOW());

-- ----------------------------
-- Product Attribute Value (Variant Level)
-- ----------------------------
INSERT INTO product_attribute_value
(id, product_id, product_variant_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, attribute_label, value, created_at)
VALUES
-- iPhone 14 Variants
(101, 1, 101, 2, 5, 'color', 'Color', 'Silver', NOW()),
(102, 1, 102, 2, 6, 'color', 'Color', 'Black', NOW()),
(103, 1, 101, 3, 9, 'storage', 'Storage', '128GB', NOW()),
(104, 1, 102, 3, 9, 'storage', 'Storage', '128GB', NOW()),
(105, 1, 103, 3, 10, 'storage', 'Storage', '256GB', NOW()),
-- iPhone 14 Pro Variants
(201, 2, 201, 2, 5, 'color', 'Color', 'Silver', NOW()),
(202, 2, 202, 2, 7, 'color', 'Color', 'Graphite', NOW()),
(203, 2, 201, 3, 9, 'storage', 'Storage', '128GB', NOW()),
(204, 2, 202, 3, 9, 'storage', 'Storage', '128GB', NOW()),
(205, 2, 203, 3, 10, 'storage', 'Storage', '256GB', NOW()),
-- iPhone 14 Pro Max Variants
(301, 3, 301, 2, 5, 'color', 'Color', 'Silver', NOW()),
(302, 3, 301, 3, 9, 'storage', 'Storage', '128GB', NOW()),
(303, 3, 302, 3, 10, 'storage', 'Storage', '256GB', NOW());




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
