
use productdb;

INSERT INTO product_master 
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap, 
 category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot, 
 currency, amount, mrp, discount, tax, price_effective_from, price_effective_to, 
 is_trending, is_featured, created_at, updated_at) 
VALUES  
(1, 'iPhone 14', 'https://example.com/images/iphone14.png', TRUE, 5, 120, 101, 3, 'Electronics', 'Mobile Phones', 'INR', 79900, 89900, 0, 18, NOW(), NULL, TRUE, TRUE, NOW(), NOW()),  
(2, 'Cargo Jeans', 'https://example.com/images/abc_jeans.png', TRUE, 4, 80, 102, 7, 'Clothing', 'Men', 'INR', 1999, 2499, 20, 5, NOW(), NULL, FALSE, TRUE, NOW(), NOW()),  
(3, 'Yoga Mat', 'https://example.com/images/yoga_mat.png', TRUE, 4, 45, 103, 10, 'Fitness', 'Equipment', 'INR', 1200, 1500, 10, 5, NOW(), NULL, FALSE, FALSE, NOW(), NOW()),  
(4, 'Samsung Galaxy S23', 'https://example.com/images/samsung_s23.png', TRUE, 5, 98, 101, 4, 'Electronics', 'Mobile Phones', 'INR', 74999, 79999, 5, 18, NOW(), NULL, TRUE, FALSE, NOW(), NOW()),
(5, 'Dell Inspiron 15', 'https://example.com/images/dell_inspiron15.png', TRUE, 5, 210,
 101, 2, 'Electronics', 'Laptops', 'INR',
 52999, 59999, 10, 18, NOW(), NULL, TRUE, FALSE, NOW(), NOW()),

-- HP Pavilion 14
(6, 'HP Pavilion 14', 'https://example.com/images/hp_pavilion14.png', TRUE, 4, 185,
 101, 2, 'Electronics', 'Laptops', 'INR',
 56999, 62999, 8, 18, NOW(), NULL, FALSE, TRUE, NOW(), NOW());



INSERT INTO product_variant_master 
(id, product_master_id, name, amount, mrp, currency, discount, tax, price_effective_from, price_effective_to, stock_quantity, is_default)
VALUES
(1, 1, 'iPhone 14 - 128GB', 79900.00, 84900.00, 'INR', 0, 18, NOW(), NULL, 50, TRUE),
(2, 1, 'iPhone 14 - 256GB', 89900.00, 94900.00, 'INR', 0, 18, NOW(), NULL, 30, FALSE),
(3, 2, 'Cargo Jeans - Size M', 1999.00, 2499.00, 'INR', 20, 5, NOW(), NULL, 100, TRUE),
(4, 2, 'Cargo Jeans - Size L', 1999.00, 2499.00, 'INR', 20, 5, NOW(), NULL, 80, FALSE),
(5, 4, 'Samsung Galaxy S23 - 128GB', 74999.00, 79999.00, 'INR', 5, 18, NOW(), NULL, 60, TRUE),
(6, 4, 'Samsung Galaxy S23 - 256GB', 79999.00, 84999.00, 'INR', 5, 18, NOW(), NULL, 40, FALSE),
-- Dell Inspiron 15
(8, 5, 'Dell Inspiron 15 - i5 / 8GB / 512GB SSD', 52999, 59999, 'INR', 10, 18, NOW(), NULL, 40, TRUE),
(9, 5, 'Dell Inspiron 15 - i7 / 16GB / 512GB SSD', 67999, 74999, 'INR', 10, 18, NOW(), NULL, 25, FALSE),

-- HP Pavilion 14
(10, 6, 'HP Pavilion 14 - i5 / 8GB / 512GB SSD', 56999, 62999, 'INR', 8, 18, NOW(), NULL, 50, TRUE),
(11, 6, 'HP Pavilion 14 - i7 / 16GB / 1TB SSD', 71999, 78999, 'INR', 8, 18, NOW(), NULL, 30, FALSE);

-- Note: Yoga Mat has no variants

INSERT INTO product_attribute_value 
(id, product_id, product_variant_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, attribute_label, value, created_at, attribute_group_name_snap)
VALUES
--Basic Info (Product Level)
(1, 1, NULL, 1, NULL, 'brand', 'Brand', 'Apple', NOW(), 'Basic Info'),
(2, 1, NULL, 2, NULL, 'model_name', 'Model Name', 'iPhone 14', NOW(), 'Basic Info'),
(3, 1, NULL, 3, NULL, 'in_the_box', 'In the Box', 'Handset, USB-C Cable, Documentation', NOW(), 'Basic Info'),

-- Technical Details (Product Level)
(4, 1, NULL, 4, NULL, 'processor', 'Processor', 'A15 Bionic Chip', NOW(), 'Technical Details'),
(5, 1, NULL, 5, NULL, 'display', 'Display', '6.1-inch Super Retina XDR', NOW(), 'Technical Details'),
(6, 1, NULL, 6, NULL, 'battery', 'Battery', '3227 mAh', NOW(), 'Technical Details');

INSERT INTO product_attribute_value 
(id, product_id, product_variant_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, attribute_label, value, created_at, attribute_group_name_snap)
VALUES
-- Variant Info: Color
(7, NULL, 1, 10, 1, 'color', 'Color', 'Black', NOW(), 'Variant Info'),
(8, NULL, 2, 10, 2, 'color', 'Color', 'White', NOW(), 'Variant Info'),

-- Variant Info: Storage
(9, NULL, 1, 11, 3, 'storage', 'Storage', '128 GB', NOW(), 'Variant Info'),
(10, NULL, 2, 11, 3, 'storage', 'Storage', '128 GB', NOW(), 'Variant Info'),
	
-- Technical Info (Variant-specific if applicable)
(11, NULL, 1, 12, NULL, 'sar_value', 'SAR Value', '1.18 W/kg', NOW(), 'Technical Info'),
(12, NULL, 2, 12, NULL, 'sar_value', 'SAR Value', '1.18 W/kg', NOW(), 'Technical Info'),

-- Variant-specific Model Codes (Real-world)
(13, NULL, 1, 14, NULL, 'model_number', 'Model Number', 'A2882', NOW(), 'Variant Info'),
(14, NULL, 2, 14, NULL, 'model_number', 'Model Number', 'A2883', NOW(), 'Variant Info'),

-- Basic Info (Product Level)
(15, 4, NULL, 1, NULL, 'brand', 'Brand', 'Samsung', NOW(), 'Basic Info'),
(16, 4, NULL, 2, NULL, 'model_name', 'Model Name', 'Galaxy S23', NOW(), 'Basic Info'),
(17, 4, NULL, 3, NULL, 'in_the_box', 'In the Box', 'Handset, USB-C Cable, Documentation', NOW(), 'Basic Info'),

-- Technical Details (Product Level)
(18, 4, NULL, 4, NULL, 'processor', 'Processor', 'Snapdragon 8 Gen 2', NOW(), 'Technical Details'),
(19, 4, NULL, 5, NULL, 'display', 'Display', '6.1-inch Dynamic AMOLED 2X', NOW(), 'Technical Details'),
(20, 4, NULL, 6, NULL, 'battery', 'Battery', '3900 mAh', NOW(), 'Technical Details'),

(21, NULL, 5, 10, 1, 'color', 'Color', 'Phantom Black', NOW(), 'Variant Info'),
(22, NULL, 6, 10, 2, 'color', 'Color', 'Green', NOW(), 'Variant Info'),

-- Variant Info: Storage
(23, NULL, 5, 11, 3, 'storage', 'Storage', '128 GB', NOW(), 'Variant Info'),
(24, NULL, 6, 11, 4, 'storage', 'Storage', '256 GB', NOW(), 'Variant Info'),

-- Technical Info (Variant-specific)
(25, NULL, 5, 12, NULL, 'sar_value', 'SAR Value', '1.03 W/kg', NOW(), 'Technical Info'),
(26, NULL, 6, 12, NULL, 'sar_value', 'SAR Value', '1.03 W/kg', NOW(), 'Technical Info'),

-- Variant-specific Model Codes
(27, NULL, 5, 14, NULL, 'model_number', 'Model Number', 'SM-S911B/DS', NOW(), 'Variant Info'),
(28, NULL, 6, 14, NULL, 'model_number', 'Model Number', 'SM-S911E/DS', NOW(), 'Variant Info'),

-- Product Level
(29, 5, NULL, 1, NULL, 'brand', 'Brand', 'Dell', NOW(), 'Basic Info'),
(30, 5, NULL, 2, NULL, 'model_name', 'Model Name', 'Inspiron 15', NOW(), 'Basic Info'),
(31, 5, NULL, 3, NULL, 'in_the_box', 'In the Box', 'Laptop, Charger, Documentation', NOW(), 'Basic Info'),
(32, 5, NULL, 4, NULL, 'processor', 'Processor', 'Intel 12th Gen', NOW(), 'Technical Details'),
(33, 5, NULL, 5, NULL, 'display', 'Display', '15.6-inch FHD Anti-glare', NOW(), 'Technical Details'),
(34, 5, NULL, 6, NULL, 'battery', 'Battery', '54 Wh', NOW(), 'Technical Details'),

-- Variants
(35, NULL, 8, 10, 1, 'color', 'Color', 'Silver', NOW(), 'Variant Info'),
(36, NULL, 9, 10, 1, 'color', 'Color', 'Silver', NOW(), 'Variant Info'),

(37, NULL, 8, 11, 3, 'ram', 'RAM', '8 GB', NOW(), 'Variant Info'),
(38, NULL, 9, 11, 4, 'ram', 'RAM', '16 GB', NOW(), 'Variant Info'),

(39, NULL, 8, 14, NULL, 'model_number', 'Model Number', 'INS15-1234', NOW(), 'Variant Info'),
(40, NULL, 9, 14, NULL, 'model_number', 'Model Number', 'INS15-5678', NOW(), 'Variant Info'),

-- Product Level
(41, 6, NULL, 1, NULL, 'brand', 'Brand', 'HP', NOW(), 'Basic Info'),
(42, 6, NULL, 2, NULL, 'model_name', 'Model Name', 'Pavilion 14', NOW(), 'Basic Info'),
(43, 6, NULL, 3, NULL, 'in_the_box', 'In the Box', 'Laptop, Charger, Documentation', NOW(), 'Basic Info'),
(44, 6, NULL, 4, NULL, 'processor', 'Processor', 'Intel 12th Gen', NOW(), 'Technical Details'),
(45, 6, NULL, 5, NULL, 'display', 'Display', '14-inch IPS FHD', NOW(), 'Technical Details'),
(46, 6, NULL, 6, NULL, 'battery', 'Battery', '43 Wh', NOW(), 'Technical Details'),

-- Variants
(47, NULL, 10, 10, 1, 'color', 'Color', 'Blue', NOW(), 'Variant Info'),
(48, NULL, 11, 10, 2, 'color', 'Color', 'Silver', NOW(), 'Variant Info'),

(49, NULL, 10, 11, 3, 'ram', 'RAM', '8 GB', NOW(), 'Variant Info'),
(50, NULL, 11, 11, 4, 'ram', 'RAM', '16 GB', NOW(), 'Variant Info'),

(51, NULL, 10, 14, NULL, 'model_number', 'Model Number', 'HP14-AC123', NOW(), 'Variant Info'),
(52, NULL, 11, 14, NULL, 'model_number', 'Model Number', 'HP14-AC456', NOW(), 'Variant Info');


INSERT INTO product_image 
(id, product_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'image', 'https://example.com/images/iphone14_front.png', 'iPhone 14 Front', 1, NOW()),
(2, 1, 'image', 'https://example.com/images/iphone14_back.png', 'iPhone 14 Back', 2, NOW()),
(3, 2, 'image', 'https://example.com/images/abc_jeans_front.png', 'Cargo Jeans Front', 1, NOW()),
(4, 2, 'image', 'https://example.com/images/abc_jeans_back.png', 'Cargo Jeans Back', 2, NOW()),
(5, 3, 'image', 'https://example.com/images/yoga_mat.png', 'Yoga Mat', 1, NOW()),
(6, 4, 'image', 'https://example.com/images/s23_front.png', 'Samsung Galaxy S23 Front', 1, NOW()),
(7, 4, 'image', 'https://example.com/images/s23_back.png', 'Samsung Galaxy S23 Back', 2, NOW()),
-- Dell
(8, 5, 'image', 'https://example.com/images/dell_inspiron15_front.png', 'Dell Inspiron 15 Front', 1, NOW()),
(9, 5, 'image', 'https://example.com/images/dell_inspiron15_back.png', 'Dell Inspiron 15 Back', 2, NOW()),

-- HP
(10, 6, 'image', 'https://example.com/images/hp_pavilion14_front.png', 'HP Pavilion 14 Front', 1, NOW()),
(11, 6, 'image', 'https://example.com/images/hp_pavilion14_back.png', 'HP Pavilion 14 Back', 2, NOW());

INSERT INTO product_variant_image 
(id, product_variant_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'image', 'https://example.com/images/iphone14_128_black.png', 'iPhone 14 128GB Black', 1, NOW()),
(2, 2, 'image', 'https://example.com/images/iphone14_256_white.png', 'iPhone 14 256GB White', 1, NOW()),
(3, 5, 'image', 'https://example.com/images/s23_128_black.png', 'Samsung Galaxy S23 128GB Phantom Black', 1, NOW()),
(4, 6, 'image', 'https://example.com/images/s23_256_green.png', 'Samsung Galaxy S23 256GB Green', 1, NOW()),
-- Dell
(5, 8, 'image', 'https://example.com/images/dell_i5_8gb.png', 'Dell Inspiron 15 i5 Variant', 1, NOW()),
(6, 9, 'image', 'https://example.com/images/dell_i7_16gb.png', 'Dell Inspiron 15 i7 Variant', 1, NOW()),

-- HP
(7, 10, 'image', 'https://example.com/images/hp_i5_8gb.png', 'HP Pavilion 14 i5 Variant', 1, NOW()),
(8, 11, 'image', 'https://example.com/images/hp_i7_16gb.png', 'HP Pavilion 14 i7 Variant', 1, NOW());


INSERT INTO product_addon
(id, product_id, product_variant_id, name, description, amount, mrp, currency, discount, tax, price_effective_from, price_effective_to, is_active, display_order, created_at, updated_at)
VALUES
-- Variant-level addons (apply to specific variants)
(1, NULL, 1, 'Screen Protector', 'Tempered Glass Screen Protector', 499.00, 599.00, 'INR', 0, 18, NOW(), NULL, TRUE, 1, NOW(), NOW()),
(2, 1, NULL, 'Fast Charger', '20W USB-C Power Adapter', 1499.00, 1799.00, 'INR', 0, 18, NOW(), NULL, TRUE, 2, NOW(), NOW()),

-- Product-level addons (apply to whole product)
(3, 2, NULL, 'Extra Button', 'Spare Jeans Button', 50.00, 60.00, 'INR', 0, 5, NOW(), NULL, TRUE, 1, NOW(), NOW()),

-- No variant product (Yoga Mat)
(4, 3, NULL, 'Carry Bag', 'Yoga Mat Carry Bag', 250.00, 300.00, 'INR', 0, 5, NOW(), NULL, TRUE, 1, NOW(), NOW()),

(5, 4, NULL, 'Samsung Care+ 1 Year', 'Extended warranty & accidental damage protection', 
 4999.00, 5499.00, 'INR', 0, 18, NOW(), NULL, TRUE, 1, NOW(), NOW()),

(6, NULL, 5, 'Back Cover', 'Soft silicone protective back cover for Galaxy S23', 
 799.00, 999.00, 'INR', 0, 18, NOW(), NULL, TRUE, 1, NOW(), NOW()),

 -- Dell Inspiron 15 Addons
(7, 5, NULL, 'Extended Warranty 2 Years', 'Covers repairs & service', 2999, 3499, 'INR', 0, 18, NOW(), NULL, TRUE, 1, NOW(), NOW()),
(8, NULL, 8, 'Laptop Sleeve', 'Protective laptop sleeve 15-inch', 899, 999, 'INR', 0, 18, NOW(), NULL, TRUE, 2, NOW(), NOW()),

-- HP Pavilion 14 Addons
(9, 6, NULL, 'Accidental Damage Protection', 'Covers drops & spills for 1 year', 2499, 2999, 'INR', 0, 18, NOW(), NULL, TRUE, 1, NOW(), NOW()),
(10, NULL, 10, 'Wireless Mouse', '2.4GHz Wireless Mouse', 599, 699, 'INR', 0, 18, NOW(), NULL, TRUE, 2, NOW(), NOW());

