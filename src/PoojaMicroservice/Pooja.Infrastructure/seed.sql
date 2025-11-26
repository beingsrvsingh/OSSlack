use poojadb;

INSERT INTO pooja_master 
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot, currency, amount, mrp, tax, discount, is_trending, is_featured, created_at, updated_at)
VALUES
(1, 'Ganesh Pooja', 'https://example.com/images/ganesh_pooja.jpg', 1, 4.5, 120, 13, 15, 'Service', 'Pooja', 'INR', 1500.00, 1800.00, 8.00, 5.00, 1, 1, NOW(), NOW()),
(2, 'Satyanarayan Pooja', 'https://example.com/images/satyanarayan_pooja.jpg', 1, 4.8, 85, 13, 15, 'Service', 'Pooja', 'INR', 2000.00, 2200.00, 10.00, 0.00, 0, 1, NOW(), NOW()),
(3, 'Lakshmi Pooja', 'https://example.com/images/lakshmi_pooja.jpg', 1, 4.2, 45, 13, 15, 'Service', 'Pooja', 'INR', 1200.00, 1400.00, 6.00, 0.00, 1, 0, NOW(), NOW()),
(4, 'Navagraha Pooja', 'https://example.com/images/navagraha_pooja.jpg', 1, 4.7, 60, 13, 15, 'Service', 'Pooja', 'INR', 3000.00, 2800.00, 12.00, 10.00, 1, 1, NOW(), NOW()),
(5, 'Vastu Pooja', 'https://example.com/images/vastu_pooja.jpg', 1, 4.0, 30, 13, 15, 'Service', 'Pooja', 'INR', 1800.00, 2000.00, 7.00, 0.00, 0, 0, NOW(), NOW()),
(6, 'Durga Pooja', 'https://example.com/images/durga_pooja.jpg', 1, 4.6, 55, 13, 15, 'Service', 'Pooja', 'INR', 2200.00, 2000.00, 9.00, 5.00, 1, 1, NOW(), NOW()),
(7, 'Saraswati Pooja', 'https://example.com/images/saraswati_pooja.jpg', 1, 4.3, 40, 13, 15, 'Service', 'Pooja', 'INR', 1000.00, 1200.00, 5.00, 0.00, 0, 1, NOW(), NOW()),
(8, 'Maha Shivaratri Pooja', 'https://example.com/images/shivaratri_pooja.jpg', 1, 4.9, 70, 13, 15, 'Service', 'Pooja', 'INR', 3500.00, 3300.00, 15.00, 7.00, 1, 1, NOW(), NOW()),
(9, 'Griha Pravesh Pooja', 'https://example.com/images/griha_pravesh.jpg', 1, 4.7, 65, 13, 15, 'Service', 'Pooja', 'INR', 2500.00, 2200.00, 10.00, 5.00, 1, 1, NOW(), NOW()),
(10, 'Ayushman Pooja', 'https://example.com/images/ayushman_pooja.jpg', 1, 4.4, 50, 13, 15, 'Service', 'Pooja', 'INR', 2000.00, 1800.00, 8.00, 0.00, 0, 1, NOW(), NOW());

INSERT INTO pooja_variant_master
(id, pooja_master_id, name, amount, mrp, currency, tax, discount, duration_minute, booking_type, is_default, stock_quantity, price_effective_from, price_effective_to)
VALUES
(1, 1, 'Ganesh Pooja', 1500.00, 1800.00, 'INR', 8.00, 5.00, 60, 'Onsite', 1, 30, NOW(), NOW()),
(2, 2, 'Satyanarayan Pooja', 2000.00, 1800.00, 'INR', 10.00, 0.00, 90, 'Onsite', 1, 40, NOW(), NOW()),
(3, 3, 'Lakshmi Pooja', 1200.00, 1400.00, 'INR', 6.00, 0.00, 45, 'Online', 1, 25, NOW(), NOW()),
(4, 4, 'Navagraha Pooja', 3000.00, 2800.00, 'INR', 12.00, 10.00, 120, 'Onsite', 1, 20, NOW(), NOW()),
(5, 5, 'Vastu Pooja', 1800.00, 2000.00, 'INR', 7.00, 0.00, 60, 'Onsite', 1, 15, NOW(), NOW()),
(6, 6, 'Durga Pooja', 2200.00, 2000.00, 'INR', 9.00, 5.00, 75, 'Onsite', 1, 22, NOW(), NOW()),
(7, 7, 'Saraswati Pooja', 1000.00, 1200.00, 'INR', 5.00, 0.00, 50, 'Online', 1, 18, NOW(), NOW()),
(8, 8, 'Maha Shivaratri Pooja', 3500.00, 3300.00, 'INR', 15.00, 7.00, 150, 'Onsite', 1, 30, NOW(), NOW()),
(9, 9, 'Griha Pravesh Pooja', 2500.00, 2200.00, 'INR', 10.00, 5.00, 105, 'Onsite', 1, 28, NOW(), NOW()),
(10, 10, 'Ayushman Pooja', 2000.00, 1800.00, 'INR', 8.00, 0.00, 80, 'Online', 1, 26, NOW(), NOW());


INSERT INTO pooja_image
(id, pooja_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Ganesh Pooja', 1, NOW()),
(2, 2, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Satyanarayan Pooja', 1, NOW()),
(3, 3, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Lakshmi Pooja', 1, NOW()),
(4, 4, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Navagraha Pooja', 1, NOW());


INSERT INTO pooja_variant_image
(id, pooja_variant_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Ganesh Pooja Ritual', 1, NOW()),
(2, 2, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Satyanarayan Ritual', 1, NOW()),
(3, 3, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Lakshmi Ritual', 1, NOW()),
(4, 4, 'image', 'https://picsum.photos/seed/picsum/200/300', 'Navagraha Ritual', 1, NOW());


INSERT INTO pooja_attribute_value
(id, pooja_variant_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, attribute_label, value, attribute_group_name_snap, created_at)
VALUES
(1, 1, 1, 1, 'Duration', 'Duration', '60 Minutes', 'Basic Info', NOW()),
(2, 1, 2, 2, 'Type', 'Pooja Type', 'Ganesh', 'Basic Info', NOW()),
(3, 2, 1, 3, 'Duration', 'Duration', '90 Minutes', 'Basic Info', NOW()),
(4, 2, 2, 4, 'Type', 'Pooja Type', 'Satyanarayan', 'Basic Info', NOW()),
(5, 3, 1, 5, 'Duration', 'Duration', '45 Minutes', 'Basic Info', NOW()),
(6, 3, 2, 6, 'Type', 'Pooja Type', 'Lakshmi', 'Basic Info', NOW()),
(7, 4, 1, 7, 'Duration', 'Duration', '120 Minutes', 'Basic Info', NOW()),
(8, 4, 2, 8, 'Type', 'Pooja Type', 'Navagraha', 'Basic Info', NOW());

INSERT INTO pooja_addon
(id, pooja_id, pooja_variant_id, name, description, amount, mrp, currency, tax, discount, created_at, updated_at, is_active)
VALUES
(1, 1, 1, 'Pooja Samagri Kit', 'Items required for Ganesh Pooja', 500.00, 600.00, 'INR', 8.00, 0.00, NOW(), NOW(), 1),
(2, 2, 2, 'Satyanarayan Prasad', 'Special prasadam offering', 300.00, 350.00, 'INR', 5.00, 0.00, NOW(), NOW(), 1),
(3, 4, 4, 'Navagraha Havan Samagri', 'Full set of havan items', 600.00, 700.00, 'INR', 10.00, 5.00, NOW(), NOW(), 1),
(4, 3, 3, 'Lakshmi Pooja Prasad', 'Special prasadam for Lakshmi Pooja', 400.00, 450.00, 'INR', 6.00, 0.00, NOW(), NOW(), 1);