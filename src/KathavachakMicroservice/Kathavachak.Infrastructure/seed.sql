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
