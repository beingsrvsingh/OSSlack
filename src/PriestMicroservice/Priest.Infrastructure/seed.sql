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
