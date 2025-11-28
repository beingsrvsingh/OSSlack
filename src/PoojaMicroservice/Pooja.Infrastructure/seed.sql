use poojadb;

INSERT INTO pooja_master
(
    id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, sub_category_id,
    category_name_snapshot, sub_category_name_snapshot, created_at, updated_at, amount, mrp,
    currency, discount, tax, price_effective_from, price_effective_to, is_trending, is_featured
)
VALUES
(1, 'Ganesh Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 5, 120, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 1500.00, 1800.00, 'INR', 5.00, 8.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 1, 1),
(2, 'Satyanarayan Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 5, 85, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 2000.00, 2200.00, 'INR', 0.00, 10.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 0, 1),
(3, 'Lakshmi Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 4, 45, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 1200.00, 1400.00, 'INR', 0.00, 6.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 1, 0),
(4, 'Navagraha Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 5, 60, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 3000.00, 2800.00, 'INR', 10.00, 12.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 1, 1),
(5, 'Vastu Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 4, 30, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 1800.00, 2000.00, 'INR', 0.00, 7.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 0, 0),
(6, 'Durga Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 5, 55, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 2200.00, 2000.00, 'INR', 5.00, 9.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 1, 1),
(7, 'Saraswati Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 4, 40, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 1000.00, 1200.00, 'INR', 0.00, 5.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 0, 1),
(8, 'Maha Shivaratri Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 5, 70, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 3500.00, 3300.00, 'INR', 7.00, 15.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 1, 1),
(9, 'Griha Pravesh Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 5, 65, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 2500.00, 2200.00, 'INR', 5.00, 10.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 1, 1),
(10, 'Ayushman Pooja', 'https://picsum.photos/seed/picsum/200/300', 1, 4, 50, 13, 15, 'Service', 'Pooja', '2025-11-26 22:41:08', '2025-11-26 22:41:08', 2000.00, 1800.00, 'INR', 0.00, 8.00, '2025-11-26 22:41:08.489054', '2025-11-26 22:41:08.489054', 0, 1);


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
(
    id, pooja_id, pooja_variant_id, catalog_attribute_id, catalog_attribute_value_id, 
    attribute_key, attribute_label, value, attribute_group_name_snap, created_at
)
VALUES
-- Pooja Level Attributes (5 per pooja)
(1, 1, NULL, 1, NULL, 'Popularity', 'Popularity', 'High', 'Basic Info', NOW()),
(2, 1, NULL, 2, NULL, 'Reviews', 'Reviews', 'Good', 'Basic Info', NOW()),
(3, 1, NULL, 3, NULL, 'Duration', 'Duration', 'Long', 'Basic Info', NOW()),
(4, 1, NULL, 4, NULL, 'Expertise', 'Expertise', 'Expert', 'Basic Info', NOW()),
(5, 1, NULL, 5, NULL, 'Difficulty', 'Difficulty', 'Easy', 'Basic Info', NOW()),

(6, 2, NULL, 1, NULL, 'Popularity', 'Popularity', 'Medium', 'Basic Info', NOW()),
(7, 2, NULL, 2, NULL, 'Reviews', 'Reviews', 'Average', 'Basic Info', NOW()),
(8, 2, NULL, 3, NULL, 'Duration', 'Duration', 'Short', 'Basic Info', NOW()),
(9, 2, NULL, 4, NULL, 'Expertise', 'Expertise', 'Advanced', 'Basic Info', NOW()),
(10, 2, NULL, 5, NULL, 'Difficulty', 'Difficulty', 'Moderate', 'Basic Info', NOW()),

(11, 3, NULL, 1, NULL, 'Popularity', 'Popularity', 'High', 'Basic Info', NOW()),
(12, 3, NULL, 2, NULL, 'Reviews', 'Reviews', 'Excellent', 'Basic Info', NOW()),
(13, 3, NULL, 3, NULL, 'Duration', 'Duration', 'Medium', 'Basic Info', NOW()),
(14, 3, NULL, 4, NULL, 'Expertise', 'Expertise', 'Expert', 'Basic Info', NOW()),
(15, 3, NULL, 5, NULL, 'Difficulty', 'Difficulty', 'Hard', 'Basic Info', NOW()),

(16, 4, NULL, 1, NULL, 'Popularity', 'Popularity', 'Low', 'Basic Info', NOW()),
(17, 4, NULL, 2, NULL, 'Reviews', 'Reviews', 'Average', 'Basic Info', NOW()),
(18, 4, NULL, 3, NULL, 'Duration', 'Duration', 'Long', 'Basic Info', NOW()),
(19, 4, NULL, 4, NULL, 'Expertise', 'Expertise', 'Intermediate', 'Basic Info', NOW()),
(20, 4, NULL, 5, NULL, 'Difficulty', 'Difficulty', 'Easy', 'Basic Info', NOW());



INSERT INTO pooja_addon
(id, pooja_id, pooja_variant_id, name, description, amount, mrp, currency, tax, discount, created_at, updated_at, is_active)
VALUES
(1, 1, 1, 'Pooja Samagri Kit', 'Items required for Ganesh Pooja', 500.00, 600.00, 'INR', 8.00, 0.00, NOW(), NOW(), 1),
(2, 2, 2, 'Satyanarayan Prasad', 'Special prasadam offering', 300.00, 350.00, 'INR', 5.00, 0.00, NOW(), NOW(), 1),
(3, 4, 4, 'Navagraha Havan Samagri', 'Full set of havan items', 600.00, 700.00, 'INR', 10.00, 5.00, NOW(), NOW(), 1),
(4, 3, 3, 'Lakshmi Pooja Prasad', 'Special prasadam for Lakshmi Pooja', 400.00, 450.00, 'INR', 6.00, 0.00, NOW(), NOW(), 1);