
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
