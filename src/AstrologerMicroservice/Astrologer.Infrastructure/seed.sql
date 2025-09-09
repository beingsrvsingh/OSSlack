use astrologerdb;

-- On astrologer_entity.name
ALTER TABLE astrologers
ADD FULLTEXT INDEX ix_astrologers_name (name);

-- On astrologer_expertises.name (package name)
ALTER TABLE astrologer_expertises
ADD FULLTEXT INDEX ix_astrologer_expertises_name (name);

-- On astrologer_expertises.category_name_snap
ALTER TABLE astrologer_expertises
ADD FULLTEXT INDEX ix_astrologer_expertises_cat_snap (category_name_snap);

-- On astrologer_expertises.sub_cat_name_snap
ALTER TABLE astrologer_expertises
ADD FULLTEXT INDEX ix_astrologer_expertises_subcat_snap (sub_cat_name_snap);

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

-- Seed data for astrologers table

INSERT INTO astrologers (
    id,
    user_id,
    name,
    thumbnail_url,
    average_rating,
    total_ratings,
    is_active,
    created_at,
    updated_at
) VALUES
(101, 'user-101-uuid', 'Raj Sharma', 'https://example.com/images/raj.jpg', 4.85, 120, 1, CURRENT_TIMESTAMP(6), NULL),
(102, 'user-102-uuid', 'Anita Verma', 'https://example.com/images/anita.jpg', 4.72, 98, 1, CURRENT_TIMESTAMP(6), NULL),
(103, 'user-103-uuid', 'Karthik Reddy', 'https://example.com/images/karthik.jpg', 4.90, 200, 1, CURRENT_TIMESTAMP(6), NULL);


-- Seed data for astrologer_languages

INSERT INTO astrologer_language(id, astrologer_id, language_id, language_name) VALUES
(1,101, 1, 'English'), -- Raj Sharma - English
(2,101, 2, 'Hindi'), -- Raj Sharma - Hindi
(3,102, 1, 'English'), -- Anita Verma - English
(4,102, 3, 'Tamil'), -- Anita Verma - Tamil
(5,103, 1, 'English'), -- Karthik Reddy - English
(6,103, 4, 'Telgu'); -- Karthik Reddy - Telugu

-- Seed data for astrologer_expertises table

INSERT INTO astrologer_expertises (
    id,
    astrologer_id,
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
(1, 101, 201, 301, 10, 'Expert', 'Vedic Astrology', 'Expert in Vedic astrology readings', 500.00, 30, 1, 'Horoscope', 'Vedic'),
(2, 102, 202, 302, 5, 'Intermediate', 'Numerology', 'Numerology readings and consultations', 300.00, 20, 1, 'Lucky Numbers', 'Numerology'),
(3, 103, 203, 303, 8, 'Advanced', 'Palmistry', 'Specialist in palm reading and hand analysis', 400.00, 25, 1, 'Hand Reading', 'Palmistry');


-- Seed data for astrologer_attribute_values

INSERT INTO astrologer_attribute_values (
    id,
    expertise_id,
    catalog_attribute_id,
    catalog_attribute_value_id,
    value,
    attribute_key,
    attribute_label,
    attribute_data_type_id,
    catalog_attribute_group_id,
    created_at
) VALUES
-- Kundli Type
(1, 1, 6001, 7001, 'Janma Kundli', 'kundli_type', 'Kundli Type', 4, 401, CURRENT_TIMESTAMP(6)),
(2, 2, 6001, 7002, 'Nadi Kundli', 'kundli_type', 'Kundli Type', 4, 401, CURRENT_TIMESTAMP(6)),
(3, 3, 6001, 7003, 'Prashna Kundli', 'kundli_type', 'Kundli Type', 4, 401, CURRENT_TIMESTAMP(6)),

-- Languages Spoken
(4, 1, 6002, 7004, 'Hindi', 'language', 'Language', 4, 402, CURRENT_TIMESTAMP(6)),
(5, 1, 6002, 7005, 'English', 'language', 'Language', 4, 402, CURRENT_TIMESTAMP(6)),
(6, 2, 6002, 7006, 'English', 'language', 'Language', 4, 402, CURRENT_TIMESTAMP(6)),
(7, 3, 6002, 7007, 'Sanskrit', 'language', 'Language', 4, 402, CURRENT_TIMESTAMP(6)),

-- Certification
(8, 1, 6003, 7008, 'Certified Vedic Astrologer', 'certification', 'Certification', 1, 403, CURRENT_TIMESTAMP(6)),
(9, 2, 6003, 7009, 'Numerology Expert', 'certification', 'Certification', 1, 403, CURRENT_TIMESTAMP(6)),

-- Specializations
(10, 1, 6004, 7010, 'Birth Chart Analysis', 'specializations', 'Specializations', 4, 404, CURRENT_TIMESTAMP(6)),
(11, 2, 6004, 7011, 'Numerology Reading', 'specializations', 'Specializations', 4, 404, CURRENT_TIMESTAMP(6)),
(12, 3, 6004, 7012, 'Marriage Matching', 'specializations', 'Specializations', 4, 404, CURRENT_TIMESTAMP(6));

-- Seed data for astrologer_consultation_mode table

INSERT INTO astrologer_consultation_mode (
    id,
    expertise_id,
    consultation_mode_id,
    consultation_mode
) VALUES
(1, 1, 1, 1),
(2, 1, 2, 2),

(3, 2, 1, 1),

(4, 3, 2, 2),

(5, 3, 3, 3);


-- Seed data for schedules table

INSERT INTO schedules (id, astrologer_id, day_of_week, start_time, end_time, is_available) VALUES
(1, 101, '1', '09:00:00', '12:00:00', true),
(2, 101, '3', '14:00:00', '18:00:00', true),
(3, 102, '2', '10:00:00', '13:00:00', true),
(4, 102, '5', '15:00:00', '17:00:00', true);


-- Seed data for time_slots table

INSERT INTO time_slots (id, schedule_id, start_utc, end_utc, is_booked) VALUES
(1, 1, '2025-09-10 09:00:00', '2025-09-10 09:30:00', false),
(2, 1, '2025-09-10 09:30:00', '2025-09-10 10:00:00', true),
(3, 1, '2025-09-10 10:00:00', '2025-09-10 10:30:00', false),
(4, 2, '2025-09-11 14:00:00', '2025-09-11 14:30:00', false),
(5, 2, '2025-09-11 14:30:00', '2025-09-11 15:00:00', false);



