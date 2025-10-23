INSERT INTO product_master (id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, subcategory_id, cat_snap, subcat_snap, created_at, updated_at) VALUES (1, 'iPhone 14', 'https://example.com/images/iphone.jpg', 1, 4.8, 1250, 1, 1001, 'Electronics', 'Smartphones', NOW(), NOW());

INSERT INTO product_master (id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, subcategory_id, cat_snap, subcat_snap, created_at, updated_at) VALUES (2, 'Aggarbati', 'https://example.com/images/aggarbati.jpg', 1, 4.3, 350, 1, 1004, 'Home & Garden', 'Decor', NOW(), NOW());



INSERT INTO product_attribute_value (
  id,
  catalog_attribute_id,
  cat_attr_val_id,
  price,
  value,
  attribute_key,
  attribute_label,
  attribute_datatype_id,
  attribute_group_id,
  created_at,
  product_id,
  is_default
)
VALUES
-- iPhone combinations
(1, 1, 1, 75000, 'Apple', 'brand', 'Brand', 1, 1, NOW(), 1, 1),
(2, 2, 5, 75000, 'Space Gray', 'color', 'Color', 1, 3, NOW(), 1, 0),
(3, 2, 6, 75000, 'Silver', 'color', 'Color', 1, 3, NOW(), 1, 0),
(4, 3, 7, 75000, 'Pro', 'size', 'Size', 1, 3, NOW(), 1, 0),
(5, 3, 8, 75000, 'Pro Plus', 'size', 'Size', 1, 3, NOW(), 1, 0),
(6, 3, 9, 75000, 'Pro Max', 'size', 'Size', 1, 3, NOW(), 1, 0),
(7, 4, 10, 75000, '32GB', 'storage', 'Storage', 1, 2, NOW(), 1, 0),
(8, 4, 11, 75000, '64GB', 'storage', 'Storage', 1, 2, NOW(), 1, 0),
(9, 4, 12, 75000, '128GB', 'storage', 'Storage', 1, 2, NOW(), 1, 0),

-- Aggarbati combinations
(10, 2, 1, 10, 'Hari Om', 'brand', 'Brand', 1, 20, NOW(), 2),
(11, 2, 21, 20, '20 sticks', 'sticks', 'Number of Sticks', 3, 20, NOW(), 2),
(12, 2, 21, 30, '10 sticks', 'sticks', 'Number of Sticks', 3, 20, NOW(), 2);

INSERT INTO product_image (
  id,
  image_url,
  sort_order,
  alt_text,
  product_attribute_value_id
)
VALUES
(1, 'https://example.com/images/iphone-front.jpg', 1, 'iPhone Front View', 1),
(2, 'https://example.com/images/iphone-back.jpg', 2, 'iPhone Back View', 1),
(3, 'https://example.com/images/iphone-side.jpg', 3, 'iPhone Side View', 1);
