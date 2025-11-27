USE kathavachakdb;

INSERT INTO kathavachak_master
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap,
 category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot,
 currency, amount, mrp, tax, discount,
 is_trending, is_featured, created_at, updated_at)
VALUES
(1, 'GuruVani', 'https://picsum.photos/seed/picsum/200/300', TRUE, 4.9, 300,
 21, 22, 'Service', 'Kathavachak',
 'INR', 2000.00, 2500.00, 10.00, 0.00,
 TRUE, TRUE, NOW(), NOW()),

(2, 'DivineVoice', 'https://picsum.photos/seed/picsum/200/300', TRUE, 4.6, 220,
 21, 22, 'Service', 'Kathavachak',
 'INR', 2100.00, 2600.00, 12.00, 5.00,
 FALSE, TRUE, NOW(), NOW());

INSERT INTO kathavachak_expertise
(id, kathavachak_id, name, amount, mrp, currency, tax, discount,
 duration_minute, booking_type, is_default, stock_quantity,
 price_effective_from, price_effective_to)
VALUES
(1, 1, 'Bhagavad Katha', 2000.00, 2500.00, 'INR', 10.00, 0.00,
 60, 'Online', TRUE, 50, NOW(), NULL),

(2, 1, 'Spiritual Discourse', 1800.00, 2200.00, 'INR', 8.00, 0.00,
 45, 'Online', TRUE, 40, NOW(), NULL),

(3, 2, 'Bhagavad Katha', 2100.00, 2600.00, 'INR', 12.00, 5.00,
 70, 'Online', TRUE, 60, NOW(), NULL),

(4, 2, 'Devotional Stories', 1700.00, 2100.00, 'INR', 7.00, 0.00,
 50, 'Online', TRUE, 45, NOW(), NULL);

INSERT INTO kathavachak_image
(id, kathavachak_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'GuruVani Profile Photo', 1, NOW()),
(2, 2, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'DivineVoice Profile Photo', 1, NOW());

INSERT INTO kathavachak_expertise_image
(id, kathavachak_expertise_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Bhagavad Katha Session', 1, NOW()),
(2, 2, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Spiritual Discourse', 1, NOW()),
(3, 3, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Bhagavad Katha Session', 1, NOW()),
(4, 4, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Devotional Stories Session', 1, NOW());

INSERT INTO kathavachak_attribute_value
(id, expertise_id, catalog_attribute_id, catalog_attribute_value_id,
 attribute_key, attribute_label, value, attribute_group_name_snap, created_at)
VALUES
(1, 1, 1, 1, 'Experience', 'Experience', '15 Years', 'Basic Info', NOW()),
(2, 1, 2, 2, 'ProficiencyLevel', 'Proficiency Level', 'Expert', 'Basic Info', NOW()),

(3, 2, 1, 3, 'Experience', 'Experience', '12 Years', 'Basic Info', NOW()),
(4, 2, 2, 4, 'ProficiencyLevel', 'Proficiency Level', 'Advanced', 'Basic Info', NOW()),

(5, 3, 1, 5, 'Experience', 'Experience', '18 Years', 'Basic Info', NOW()),
(6, 3, 2, 6, 'ProficiencyLevel', 'Proficiency Level', 'Expert', 'Basic Info', NOW()),

(7, 4, 1, 7, 'Experience', 'Experience', '10 Years', 'Basic Info', NOW()),
(8, 4, 2, 8, 'ProficiencyLevel', 'Proficiency Level', 'Intermediate', 'Basic Info', NOW());

INSERT INTO kathavachak_addon
(id, kathavachak_id, kathavachak_expertise_id, name, description,
 amount, mrp, currency, tax, discount,
 is_active, display_order, created_at, updated_at)
VALUES
(1, 1, 1, 'Printed Katha Book', 'Bhagavad Katha book PDF and print copy',
 300.00, 400.00, 'INR', 5.00, 0.00,
 TRUE, 1, NOW(), NOW()),

(2, 1, 2, 'Audio Recording', 'Spiritual discourse recording',
 200.00, 250.00, 'INR', 5.00, 0.00,
 TRUE, 2, NOW(), NOW()),

(3, 2, 3, 'Bhagavad Katha Book', 'Full Bhagavad Katha book',
 350.00, 450.00, 'INR', 5.00, 0.00,
 TRUE, 1, NOW(), NOW()),

(4, 2, 4, 'Devotional Song Pack', 'Audio songs for devotional stories',
 250.00, 320.00, 'INR', 5.00, 0.00,
 TRUE, 2, NOW(), NOW());
