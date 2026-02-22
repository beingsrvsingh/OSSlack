use priestdb;

-- On astrologer_entity.name
ALTER TABLE priests
ADD FULLTEXT INDEX ix_priests_name (name);

-- On priest_expertises.name (package name)
ALTER TABLE priest_expertises
ADD FULLTEXT INDEX ix_priest_expertises_name (name);

-- On priest_expertises.category_name_snap
ALTER TABLE priest_expertises
ADD FULLTEXT INDEX ix_priest_expertises_cat_snap (category_name_snap);

-- On priest_expertises.sub_cat_name_snap
ALTER TABLE priest_expertises
ADD FULLTEXT INDEX ix_priest_expertises_subcat_snap (sub_cat_name_snap);

-- Seed data for language_master table

INSERT INTO language_master (id, language_name, language_code, display_order) VALUES
(1, 'English', 'EN', 1),
(2, 'Hindi', 'HI', 2),
(3, 'Tamil', 'TA', 3),
(4, 'Telugu', 'TE', 4);

-- Seed data for consultation_mode_master table

INSERT INTO consultation_mode_master (id, mode, display_order) VALUES
(1, 'Chat', 1),
(2, 'Call', 2),
(3, 'Video Call', 3),
(4, 'Email', 4);

-- Sample seed for priests table

INSERT INTO priests (
    id,
    user_id,
    temple_id,
    name,
    thumbnail_url,
    average_rating,
    total_ratings,
    is_active,
    created_at,
    updated_at
) VALUES
-- Priest 1
(101, 'usr-001', 'tpl-001', 'Pandit Ram Sharma', 'https://example.com/img/ram.jpg', 4.5, 120, TRUE, CURRENT_TIMESTAMP(6), NULL),

-- Priest 2
(102, 'usr-002', 'tpl-002', 'Pandit Suresh Joshi', 'https://example.com/img/suresh.jpg', 4.2, 80, TRUE, CURRENT_TIMESTAMP(6), NULL),

-- Priest 3
(103, 'usr-003', 'tpl-003', 'Pandit Arvind Shastri', NULL, 4.8, 150, TRUE, CURRENT_TIMESTAMP(6), NULL);


-- Seed data for priest_language

INSERT INTO priest_language (
    id,
    priest_id,
    language_id,
    language_name
) VALUES
(1, 101, 1, 'Hindi'),
(2, 101, 2, 'Sanskrit'),
(3, 102, 1, 'Hindi'),
(4, 102, 3, 'Marathi'),
(5, 103, 2, 'Sanskrit');

-- Seed data for priest_expertises

INSERT INTO priest_expertises (
    id,
    priest_id,
    category_id,
    sub_category_id,
    years_of_experience,
    proficiency_level,
    name,
    description,
    price,
    duration,
    is_active,
    sub_cat_name_snap,
    category_name_snap
) VALUES
(1, 101, 1, 11, 10, 'Expert', 'Satyanarayan Pooja', 'Complete Satyanarayan katha with havan', 1500.00, 1, TRUE, 'Satyanarayan Pooja', 'Pooja'),

(2, 101, 2, 21, 8, 'Intermediate', 'Griha Pravesh', 'Vedic griha pravesh ceremony for new home', 2500.00, 2, TRUE, 'Griha Pravesh', 'Vastu'),

(3, 102, 1, 12, 15, 'Expert', 'Mundan Sanskar', 'Ceremony for child’s first haircut', 1200.00, 3, TRUE, 'Mundan Sanskar', 'Sanskar'),

(4, 103, 3, 31, 20, 'Advanced', 'Shraddha Pooja', 'Annual ritual for ancestors', 1800.00, 4, TRUE, 'Shraddha Pooja', 'Rituals');

-- Seed data for attribute_values

INSERT INTO attribute_values (
    id,
    expertiese_id,
    catalog_attribute_id,
    catalog_attribute_value_id,
    value,
    attribute_key,
    attribute_label,
    attribute_data_type_id,
    catalog_attribute_group_id,
    created_at
) VALUES
-- Example: Priest offers online puja
(1, 1, 1001, 2001, 'Yes', 'offers_online_puja', 'Offers Online Puja', 1, 301, CURRENT_TIMESTAMP(6)),

-- Example: Experience level
(2, 1, 1002, 2002, '15', 'years_of_experience', 'Years of Experience', 2, 301, CURRENT_TIMESTAMP(6)),

-- Example: Certification status
(3, 2, 1003, 2003, 'Certified', 'certification_status', 'Certification Status', 3, 302, CURRENT_TIMESTAMP(6));

-- Seed data for consultation_mode

INSERT INTO consultation_mode (
    id,
    expertise_id,
    consultation_mode_id,
    mode
) VALUES
(1, 1, 1, 1),
(2, 1, 2, 2),

(3, 2, 1, 1),

(4, 3, 2, 2),

(5, 4, 3, 3);

INSERT INTO schedule (
    id,
    priest_id,
    day_of_week,
    date,
    is_available
) VALUES
(1, 101, 1, '2025-03-03', TRUE),  -- Monday
(2, 101, 2, '2025-03-03', TRUE),  -- Tuesday
(3, 101, 3, '2025-03-03', FALSE), -- Wednesday (Not available)
(4, 102, 5, '2025-03-03', TRUE),  -- Friday
(5, 102, 6, '2025-03-03', TRUE);  -- Saturday


INSERT INTO time_slot (
    id,
    schedule_id,
    start_time,
    end_time,
    is_booked
) VALUES
-- For schedule_id = 1 (Monday)
(1, 1, '2025-09-15 09:00:00', '2025-09-15 09:30:00', FALSE),
(2, 1, '2025-09-15 09:30:00', '2025-09-15 10:00:00', FALSE),

-- For schedule_id = 2 (Tuesday)
(3, 2, '2025-09-16 10:00:00', '2025-09-16 10:30:00', TRUE),
(4, 2, '2025-09-16 10:30:00', '2025-09-16 11:00:00', FALSE),

-- For schedule_id = 5 (Saturday)
(5, 5, '2025-09-20 08:00:00', '2025-09-20 08:30:00', FALSE),
(6, 5, '2025-09-20 08:30:00', '2025-09-20 09:00:00', FALSE);


------------------------------------------Priest-------------------------------------------------------------
use priestdb;

SET FOREIGN_KEY_CHECKS = 0;
DELETE FROM priest_master;
SET FOREIGN_KEY_CHECKS = 1;

INSERT INTO priest_master
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, sub_category_id,
 category_name_snapshot, sub_category_name_snapshot, currency, amount, mrp, tax, discount,
 is_trending, is_featured, created_at, updated_at)
VALUES
(1, 'PanditRaghav', 'https://picsum.photos/seed/priest1/200/300', TRUE, 4.9, 320, 26, 28,
 'Service', 'Priest', 'INR', 1500.00, 1800.00, 8.00, 5.00, TRUE, TRUE, NOW(), NOW()),

(2, 'PanditSharma', 'https://picsum.photos/seed/priest1/200/300', TRUE, 4.7, 210, 26, 28,
 'Service', 'Priest', 'INR', 1800.00, 2100.00, 8.00, 0.00, FALSE, TRUE, NOW(), NOW());
 
 INSERT INTO priest_expertise
(id, priest_id, name, amount, mrp, currency, tax, discount, duration_minute,
 booking_type, is_default, stock_quantity, price_effective_from, price_effective_to)
VALUES
(1, 1, 'Ganesh Pooja', 1500.00, 1800.00, 'INR', 8.00, 5.00, 30, 'Onsite', TRUE, 20, NOW(), NOW()),
(2, 1, 'Lakshmi Pooja', 1800.00, 2100.00, 'INR', 8.00, 0.00, 40, 'Onsite', TRUE, 18, NOW(), NOW()),
(3, 2, 'Navagraha Pooja', 2000.00, 2400.00, 'INR', 10.00, 5.00, 50, 'Onsite', TRUE, 15, NOW(), NOW()),
(4, 2, 'Saraswati Pooja', 1700.00, 2000.00, 'INR', 7.00, 0.00, 35, 'Onsite', TRUE, 22, NOW(), NOW());


INSERT INTO priest_image
(id, priest_id, media_type, image_url, alt_text, sort_order, created_at, updated_at)
VALUES
(1, 1, 'image', 'https://picsum.photos/seed/priest1/200/300', 'Pandit Raghav Profile Photo', 1, NOW(), NOW()),
(2, 2, 'image', 'https://picsum.photos/seed/priest1/200/300', 'Pandit Sharma Profile Photo', 1, NOW(), NOW());


INSERT INTO priest_expertise_image
(id, priest_expertise_id, media_type, image_url, alt_text, sort_order, created_at, updated_at)
VALUES
(1, 1, 'image', 'https://picsum.photos/seed/priest1/200/300', 'Ganesh Pooja Performance', 1, NOW(), NOW()),
(2, 2, 'image', 'https://picsum.photos/seed/priest1/200/300', 'Lakshmi Pooja Performance', 1, NOW(), NOW()),
(3, 3, 'image', 'https://picsum.photos/seed/priest1/200/300', 'Navagraha Pooja Performance', 1, NOW(), NOW()),
(4, 4, 'image', 'https://picsum.photos/seed/priest1/200/300', 'Saraswati Pooja Performance', 1, NOW(), NOW());



INSERT INTO priest_attribute_value (id,priest_id,expertise_id,catalog_attribute_id,catalog_attribute_value_id,attribute_key,attribute_label,value,created_at,attribute_group_name_snap
)
VALUES
-- ===========================================================
-- PRIEST 1 — BASIC INFO (like product-level attributes)
-- ===========================================================
(1, 1, NULL, 1, 1, 'full_name', 'Full Name', 'Pandit Rajesh Sharma', NOW(), 'Basic Info'),
(2, 1, NULL, 2, NULL, 'language', 'Languages Known', 'Hindi, English, Sanskrit', NOW(), 'Basic Info'),
(3, 1, NULL, 3, NULL, 'priest_level', 'Priest Level', 'Senior', NOW(), 'Basic Info'),
(4, 1, NULL, 4, NULL, 'availability', 'Availability', 'Morning', NOW(), 'Basic Info'),

-- ===========================================================
-- PRIEST 1 — EXPERTISE 1 (Variant-style)
-- ===========================================================
(5, 1, 1, 10, 1, 'experience', 'Experience', '12 Years', NOW(), 'Expertise Info'),
(6, 1, 1, 11, 2, 'specialization', 'Specialization', 'Ganesh Pooja', NOW(), 'Expertise Info'),
(7, 1, 1, 12, NULL, 'expertise_code', 'Expertise Code', 'EXP-GANESH-01', NOW(), 'Expertise Info'),

-- ===========================================================
-- PRIEST 1 — EXPERTISE 2 (Variant-style)
-- ===========================================================
(8, 1, 2, 10, 3, 'experience', 'Experience', '10 Years', NOW(), 'Expertise Info'),
(9, 1, 2, 11, 4, 'specialization', 'Specialization', 'Lakshmi Pooja', NOW(), 'Expertise Info'),
(10, 1, 2, 12, NULL, 'expertise_code', 'Expertise Code', 'EXP-LAXMI-02', NOW(), 'Expertise Info'),

-- ===========================================================
-- PRIEST 2 — BASIC INFO
-- ===========================================================
(11, 2, NULL, 1, 2, 'full_name', 'Full Name', 'Pandit Suresh Iyer', NOW(), 'Basic Info'),
(12, 2, NULL, 2, NULL, 'language', 'Languages Known', 'Tamil, English', NOW(), 'Basic Info'),
(13, 2, NULL, 3, NULL, 'priest_level', 'Priest Level', 'Intermediate', NOW(), 'Basic Info'),
(14, 2, NULL, 4, NULL, 'availability', 'Availability', 'Evening', NOW(), 'Basic Info'),

-- ===========================================================
-- PRIEST 2 — EXPERTISE 3
-- ===========================================================
(15, 2, 3, 10, 5, 'experience', 'Experience', '15 Years', NOW(), 'Expertise Info'),
(16, 2, 3, 11, 6, 'specialization', 'Specialization', 'Navagraha Pooja', NOW(), 'Expertise Info'),
(17, 2, 3, 12, NULL, 'expertise_code', 'Expertise Code', 'EXP-NAVA-03', NOW(), 'Expertise Info');


INSERT INTO priest_addon
(id, priest_id, priest_expertise_id, name, description, amount, mrp, currency,
 tax, discount, is_active, display_order, created_at, updated_at)
VALUES
(1, 1, 1, 'Pooja Samagri Kit', 'All required items for Ganesh Pooja', 
 500.00, 600.00, 'INR', 8.00, 0.00, TRUE, 1, NOW(), NOW()),

(2, 1, 2, 'Lakshmi Prasad', 'Special Prasad for Lakshmi Pooja',
 300.00, 350.00, 'INR', 5.00, 0.00, TRUE, 2, NOW(), NOW()),

(3, 2, 3, 'Navagraha Havan Materials', 'Complete Havan material set',
 600.00, 700.00, 'INR', 10.00, 5.00, TRUE, 1, NOW(), NOW()),

(4, 2, 4, 'Saraswati Pooja Book', 'Booklet with Saraswati Pooja details',
 250.00, 300.00, 'INR', 5.00, 0.00, TRUE, 2, NOW(), NOW());

 
-- Fixed Long Timing
INSERT INTO schedules (id, priest_id, day_of_week, start_time, end_time, is_available)
VALUES
(1, 2, 1,    '10:00:00', '19:00:00', TRUE),
(2, 2, 2,   '10:00:00', '19:00:00', TRUE),
(3, 2, 3, '10:00:00', '19:00:00', TRUE),
(4, 2, 4,  '10:00:00', '19:00:00', TRUE),
(5, 2, 5,    '10:00:00', '19:00:00', TRUE),
(6, 2, 6,  '10:00:00', '19:00:00', TRUE),
(7, 2, 7,    '10:00:00', '19:00:00', TRUE);

-- Morning And Evening Shift
INSERT INTO schedules (id, priest_id, day_of_week, start_time, end_time, is_available)
VALUES
(8, 1, 1, '10:00:00', '14:00:00', TRUE),
(9, 1, 1, '17:00:00', '19:00:00', TRUE);

-- Single Shift
INSERT INTO schedules (id, priest_id, day_of_week, start_time, end_time, is_available)
VALUES
(10, 1, 3, '09:00:00', '13:00:00', TRUE);

-- Emergency 1 Hour Break
INSERT INTO schedule_exceptions (id, priest_id, date, start_time, end_time, is_blocked)
VALUES
(1, 1, '2026-02-24', '11:00:00', '12:00:00', TRUE);

-- Full Day Off
INSERT INTO schedule_exceptions (id, priest_id, date, start_time, end_time, is_blocked)
VALUES
(2, 1, '2026-02-25', NULL, NULL, TRUE);

-- Festival Closure
INSERT INTO schedule_exceptions (id, priest_id, date, start_time, end_time, is_blocked)
VALUES
(3, 2, '2026-02-26', NULL, NULL, TRUE);

