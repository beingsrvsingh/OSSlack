INSERT INTO pooja_master
(id, name, description, base_price, discounted_price, is_price_variable, duration, is_available_online,
 is_temple_required, is_home_available, preparation_time, priest_included, bring_your_own_priest_allowed,
 average_rating, total_reviews, image_url, category_id, created_at, updated_at, is_active)
VALUES
(1, 'Ganesh Pooja', 'Traditional Ganesh Pooja for auspicious occasions', 1500.00, NULL, 0, '01:00:00', 0,
 1, 1, '00:15:00', 1, 1, 4.5, 120, 'https://example.com/images/ganesh_pooja.jpg', 1, NOW(), NULL, 1),
(2, 'Satyanarayan Pooja', 'Monthly or special day pooja for prosperity', 2000.00, 1800.00, 0, '01:30:00', 0,
 1, 1, '00:20:00', 0, 1, 4.8, 85, 'https://example.com/images/satyanarayan_pooja.jpg', 1, NOW(), NULL, 1),
(3, 'Lakshmi Pooja', 'Pooja to seek blessings of Goddess Lakshmi', 1200.00, NULL, 0, '00:45:00', 0,
 1, 0, '00:10:00', 1, 0, 4.2, 45, 'https://example.com/images/lakshmi_pooja.jpg', 2, NOW(), NULL, 1),
(4, 'Navagraha Pooja', 'Pooja to appease nine planets', 3000.00, 2800.00, 1, '02:00:00', 0,
 1, 1, '00:25:00', 1, 1, 4.7, 60, 'https://example.com/images/navagraha_pooja.jpg', 3, NOW(), NULL, 1),
(5, 'Vastu Pooja', 'Pooja to bring positivity in home or office', 1800.00, NULL, 0, '01:00:00', 0,
 0, 1, '00:15:00', 0, 0, 4.0, 30, 'https://example.com/images/vastu_pooja.jpg', 4, NOW(), NULL, 1),
(6, 'Durga Pooja', 'Invoke blessings of Goddess Durga for health and prosperity', 2200.00, 2000.00, 0, '01:15:00', 0,
 1, 1, '00:20:00', 1, 1, 4.6, 55, 'https://example.com/images/durga_pooja.jpg', 2, NOW(), NULL, 1),
(7, 'Saraswati Pooja', 'For wisdom and knowledge, ideal for students', 1000.00, NULL, 0, '00:50:00', 0,
 0, 1, '00:10:00', 0, 1, 4.3, 40, 'https://example.com/images/saraswati_pooja.jpg', 2, NOW(), NULL, 1),
(8, 'Maha Shivaratri Pooja', 'Special pooja during Maha Shivaratri festival', 3500.00, 3300.00, 1, '02:30:00', 0,
 1, 0, '00:30:00', 1, 0, 4.9, 70, 'https://example.com/images/shivaratri_pooja.jpg', 3, NOW(), NULL, 1),
(9, 'Griha Pravesh Pooja', 'Housewarming pooja to bless a new home', 2500.00, 2200.00, 0, '01:45:00', 0,
 1, 1, '00:20:00', 1, 1, 4.7, 65, 'https://example.com/images/griha_pravesh.jpg', 4, NOW(), NULL, 1),
(10, 'Ayushman Pooja', 'Pooja for health and longevity', 2000.00, 1800.00, 0, '01:20:00', 0,
 1, 1, '00:15:00', 0, 1, 4.4, 50, 'https://example.com/images/ayushman_pooja.jpg', 1, NOW(), NULL, 1);


 ------------------------------------Pooja-------------------------------------------------

 use poojadb;

-- Pooja Master
INSERT INTO pooja_master
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot, currency, is_trending, is_featured, created_at)
VALUES
(1, 'ShreeVedicPooja', 'https://example.com/images/shreevedicpooja.png', TRUE, 4.8, 280, 2, 107, 'Service', 'Pooja', 'INR', TRUE, TRUE, NOW()),
(2, 'DivineRituals', 'https://example.com/images/divinerituals.png', TRUE, 4.6, 210, 2, 107, 'Service', 'Pooja', 'INR', FALSE, TRUE, NOW());

-- Pooja Expertise (like Product Variants)
INSERT INTO pooja_variant_master
(id, pooja_master_id, name, price, mrp, duration_minute, is_default, booking_type)
VALUES
(1, 1, 'Ganesh Pooja', 1500.00, 1800.00, 30, TRUE, 'Onsite'),
(2, 1, 'Lakshmi Pooja', 1800.00, 2100.00, 40, TRUE, 'Onsite'),
(3, 2, 'Navagraha Pooja', 2000.00, 2400.00, 50, TRUE, 'Onsite'),
(4, 2, 'Saraswati Pooja', 1700.00, 2000.00, 35, TRUE, 'Onsite');

-- Pooja Master Images
INSERT INTO pooja_image
(id, pooja_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://example.com/images/shreevedicpooja_profile.png', 'ShreeVedicPooja Profile Photo', 1, NOW()),
(2, 2, 'Image', 'https://example.com/images/divinerituals_profile.png', 'DivineRituals Profile Photo', 1, NOW());

-- Pooja Expertise Images
INSERT INTO pooja_variant_image
(id, pooja_variant_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://example.com/images/ganesh_pooja.png', 'Ganesh Pooja Ritual', 1, NOW()),
(2, 2, 'Image', 'https://example.com/images/lakshmi_pooja.png', 'Lakshmi Pooja Ritual', 1, NOW()),
(3, 3, 'Image', 'https://example.com/images/navagraha_pooja.png', 'Navagraha Pooja Ritual', 1, NOW()),
(4, 4, 'Image', 'https://example.com/images/saraswati_pooja.png', 'Saraswati Pooja Ritual', 1, NOW());

-- Pooja Attribute Values (Expertise-level Attributes)
INSERT INTO pooja_attribute_value
(id, pooja_variant_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, attribute_label, value, created_at)
VALUES
(1, 1, 1, 1, 'Duration', 'Duration', '30 Minutes', NOW()),
(2, 1, 2, 2, 'RitualType', 'Ritual Type', 'Ganesh Pooja', NOW()),
(3, 2, 1, 3, 'Duration', 'Duration', '40 Minutes', NOW()),
(4, 2, 2, 4, 'RitualType', 'Ritual Type', 'Lakshmi Pooja', NOW()),
(5, 3, 1, 5, 'Duration', 'Duration', '50 Minutes', NOW()),
(6, 3, 2, 6, 'RitualType', 'Ritual Type', 'Navagraha Pooja', NOW()),
(7, 4, 1, 7, 'Duration', 'Duration', '35 Minutes', NOW()),
(8, 4, 2, 8, 'RitualType', 'Ritual Type', 'Saraswati Pooja', NOW());

-- Pooja Addons (General or Expertise-specific)
INSERT INTO pooja_addon
(id, pooja_id, pooja_variant_id, name, description, price, currency, is_active, display_order, created_at)
VALUES
(1, 1, 1, 'Pooja Samagri Kit', 'All required items for Ganesh Pooja', 500.00, 'INR', TRUE, 1, NOW()),
(2, 1, 2, 'Lakshmi Pooja Prasad', 'Special Prasad distribution', 300.00, 'INR', TRUE, 2, NOW()),
(3, 2, 3, 'Navagraha Havan Materials', 'Complete Havan material set', 600.00, 'INR', TRUE, 1, NOW()),
(4, 2, 4, 'Saraswati Pooja Book', 'Booklet with Saraswati Pooja details', 250.00, 'INR', TRUE, 2, NOW());



