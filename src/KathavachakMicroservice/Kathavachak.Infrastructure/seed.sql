use kathavachakdb;

-- ✅ FULLTEXT INDEXES FOR SEARCH OPTIMIZATION
ALTER TABLE kathavachak_master
ADD FULLTEXT INDEX idx_name (name);

ALTER TABLE kathavachak_experties
ADD FULLTEXT INDEX idx_cat_snap (category_name_snap);

ALTER TABLE kathavachak_experties
ADD FULLTEXT INDEX idx_subcat_snap (sub_cat_name_snap);


-- ✅ INSERT INTO kathavachak_master
INSERT INTO kathavachak_master
    (id, user_id, name, thumbnail_url, average_rating, total_ratings, is_active, created_at, updated_at)
VALUES
    (1, 'user-1234567890abcdef1234567890abcd', 'John Doe', 'https://example.com/profile/johndoe.jpg', 4.75, 150, TRUE, UTC_TIMESTAMP(), NULL);


-- ✅ INSERT INTO kathavachak_experties
INSERT INTO kathavachak_experties
    (id, kathavachak_id, cat_id, subcat_id, yrs_of_exp, proficiency_level, description, price, duration, is_active, category_name_snap, sub_cat_name_snap)
VALUES
    (1, 1, 10, 100, 5, 'Expert', 'Story-telling', 1000, 10, TRUE, 'Storytelling', 'Folk Tales');


-- ✅ INSERT LANGUAGES
INSERT INTO languages
    (id, language_code, language_name, display_order)
VALUES
    (1, 'en', 'English', 1),
    (2, 'hi', 'Hindi', 2),
    (3, 'mr', 'Marathi', 3);


-- ✅ LINK LANGUAGE TO KATHAVACHAK
INSERT INTO kathavachak_language
    (id, kathavachak_id, language_id, language_name)
VALUES
    (1, 1, 1, 'Hindi');


-- ✅ INSERT TOPIC
INSERT INTO kathavachak_topic
    (id, kathavachak_id, topic_name)
VALUES
    (1, 1, 'Mythology');


-- ✅ INSERT SESSION MODE
INSERT INTO kathavachak_session_mode
    (id, kathavachak_id, mode_name)
VALUES
    (1, 1, 1);


-- ✅ INSERT SCHEDULE
INSERT INTO kathavachak_schedule
    (id, kathavachak_id, start_date, end_date, is_available, is_recurring)
VALUES
    (1, 1, '2025-09-10', '2025-09-11', TRUE, TRUE);


-- ✅ INSERT TIME SLOT
INSERT INTO kathavachak_time_slot
    (id, schedule_id, start_time, end_time, day_of_week, is_booked)
VALUES
    (1, 1, '10:00:00', '11:00:00', 1, FALSE);


-- ✅ INSERT MEDIA
INSERT INTO kathavachak_media
    (id, kathavachak_id, url, title, media_type, description)
VALUES
    (1, 1, 'https://example.com/media/storytelling_video.mp4', '', 1, '');


-- ✅ INSERT ATTRIBUTE VALUE (based on EF config for table: attribute_values)
INSERT INTO attribute_values
    (id, expertise_id, catalog_attribute_id, catalog_attribute_value_id, value, attribute_key, attribute_label, attribute_data_type_id, catalog_attribute_group_id, created_at)
VALUES
    (1, 1, 101, 1001, 'Advanced', 'story_level', 'Story Level', 1, NULL, UTC_TIMESTAMP());

    ------------------------------------Kathavachak -----------------------


    use kathavachakdb;

-- Kathavachak Master
INSERT INTO kathavachak_master
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap, category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot, currency, is_trending, is_featured, created_at)
VALUES
(1, 'GuruVani', 'https://example.com/images/guruvani.png', TRUE, 4.9, 300, 2, 106, 'Service', 'Kathavachak', 'INR', TRUE, TRUE, NOW()),
(2, 'DivineVoice', 'https://example.com/images/divinevoice.png', TRUE, 4.6, 220, 2, 106, 'Service', 'Kathavachak', 'INR', FALSE, TRUE, NOW());

-- Kathavachak Expertise (like Product Variants)
INSERT INTO kathavachak_expertise
(id, kathavachak_id, name, price, mrp, duration_minute, is_default, booking_type)
VALUES
(1, 1, 'Bhagavad Katha', 2000.00, 2500.00, 60, TRUE, 'Online'),
(2, 1, 'Spiritual Discourse', 1800.00, 2200.00, 45, TRUE, 'Online'),
(3, 2, 'Bhagavad Katha', 2100.00, 2600.00, 70, TRUE, 'Online'),
(4, 2, 'Devotional Stories', 1700.00, 2100.00, 50, TRUE, 'Online');

-- Kathavachak Master Images
INSERT INTO kathavachak_image
(id, kathavachak_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://example.com/images/guruvani_profile.png', 'GuruVani Profile Photo', 1, NOW()),
(2, 2, 'Image', 'https://example.com/images/divinevoice_profile.png', 'DivineVoice Profile Photo', 1, NOW());

-- Kathavachak Expertise Images
INSERT INTO kathavachak_expertise_image
(id, kathavachak_expertise_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://example.com/images/bhagavad_katha.png', 'Bhagavad Katha Session', 1, NOW()),
(2, 2, 'Image', 'https://example.com/images/spiritual_discourse.png', 'Spiritual Discourse', 1, NOW()),
(3, 3, 'Image', 'https://example.com/images/bhagavad_katha2.png', 'Bhagavad Katha Session', 1, NOW()),
(4, 4, 'Image', 'https://example.com/images/devotional_stories.png', 'Devotional Stories Session', 1, NOW());

-- Kathavachak Attribute Values (Expertise-level Attributes)
INSERT INTO kathavachak_attribute_value
(id, expertise_id, catalog_attribute_id, catalog_attribute_value_id, attribute_key, attribute_label, value, created_at)
VALUES
(1, 1, 1, 1, 'Experience', 'Experience', '15 Years', NOW()),
(2, 1, 2, 2, 'ProficiencyLevel', 'Proficiency Level', 'Expert', NOW()),
(3, 2, 1, 3, 'Experience', 'Experience', '12 Years', NOW()),
(4, 2, 2, 4, 'ProficiencyLevel', 'Proficiency Level', 'Advanced', NOW()),
(5, 3, 1, 5, 'Experience', 'Experience', '18 Years', NOW()),
(6, 3, 2, 6, 'ProficiencyLevel', 'Proficiency Level', 'Expert', NOW()),
(7, 4, 1, 7, 'Experience', 'Experience', '10 Years', NOW()),
(8, 4, 2, 8, 'ProficiencyLevel', 'Proficiency Level', 'Intermediate', NOW());

-- Kathavachak Addons (General or Expertise-specific)
INSERT INTO kathavachak_addon
(id, kathavachak_id, kathavachak_expertise_id, name, description, price, currency, is_active, display_order, created_at)
VALUES
(1, 1, 1, 'Printed Katha Book', 'Bhagavad Katha book PDF and print copy', 300.00, 'INR', TRUE, 1, NOW()),
(2, 1, 2, 'Audio Recording', 'Spiritual discourse recording', 200.00, 'INR', TRUE, 2, NOW()),
(3, 2, 3, 'Bhagavad Katha Book', 'Full Bhagavad Katha book', 350.00, 'INR', TRUE, 1, NOW()),
(4, 2, 4, 'Devotional Song Pack', 'Audio songs for devotional stories', 250.00, 'INR', TRUE, 2, NOW());

