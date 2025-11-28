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

INSERT INTO kathavachak_attribute_value (
    id,
    kathavachak_id,
    expertise_id,
    catalog_attribute_id,
    catalog_attribute_value_id,
    attribute_key,
    attribute_label,
    value,
    attribute_group_name_snap,
    created_at
)
VALUES
-- ===========================================================
-- KATHAVACHAK 1 — BASIC INFO
-- ===========================================================
(1, 1, NULL, 1, NULL, 'full_name', 'Full Name', 'Kathavachak Ramesh Sharma', 'Basic Info', NOW()),
(2, 1, NULL, 2, NULL, 'languages', 'Languages Known', 'Hindi, English', 'Basic Info', NOW()),
(3, 1, NULL, 3, NULL, 'rating', 'Rating', '4.9', 'Basic Info', NOW()),

-- ===========================================================
-- KATHAVACHAK 1 — EXPERTISE 1
-- ===========================================================
(4, 1, 1, 10, 1, 'experience', 'Experience', '15 Years', 'Expertise Info', NOW()),
(5, 1, 1, 11, 2, 'proficiency_level', 'Proficiency Level', 'Expert', 'Expertise Info', NOW()),
(6, 1, 1, 12, NULL, 'expertise_code', 'Expertise Code', 'KATHA-EXP-01', 'Expertise Info', NOW()),

-- ===========================================================
-- KATHAVACHAK 1 — EXPERTISE 2
-- ===========================================================
(7, 1, 2, 10, 3, 'experience', 'Experience', '12 Years', 'Expertise Info', NOW()),
(8, 1, 2, 11, 4, 'proficiency_level', 'Proficiency Level', 'Advanced', 'Expertise Info', NOW()),
(9, 1, 2, 12, NULL, 'expertise_code', 'Expertise Code', 'KATHA-ADV-02', 'Expertise Info', NOW()),

-- ===========================================================
-- KATHAVACHAK 2 — BASIC INFO
-- ===========================================================
(10, 2, NULL, 1, NULL, 'full_name', 'Full Name', 'Kathavachak Suresh Iyer', 'Basic Info', NOW()),
(11, 2, NULL, 2, NULL, 'languages', 'Languages Known', 'Tamil, English', 'Basic Info', NOW()),
(12, 2, NULL, 3, NULL, 'rating', 'Rating', '4.7', 'Basic Info', NOW()),

-- ===========================================================
-- KATHAVACHAK 2 — EXPERTISE 3
-- ===========================================================
(13, 2, 3, 10, 5, 'experience', 'Experience', '18 Years', 'Expertise Info', NOW()),
(14, 2, 3, 11, 6, 'proficiency_level', 'Proficiency Level', 'Expert', 'Expertise Info', NOW()),
(15, 2, 3, 12, NULL, 'expertise_code', 'Expertise Code', 'KATHA-EXP-03', 'Expertise Info', NOW()),

-- ===========================================================
-- KATHAVACHAK 2 — EXPERTISE 4
-- ===========================================================
(16, 2, 4, 10, 7, 'experience', 'Experience', '10 Years', 'Expertise Info', NOW()),
(17, 2, 4, 11, 8, 'proficiency_level', 'Proficiency Level', 'Intermediate', 'Expertise Info', NOW()),
(18, 2, 4, 12, NULL, 'expertise_code', 'Expertise Code', 'KATHA-INT-04', 'Expertise Info', NOW());


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
