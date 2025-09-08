use kathavachakdb;


-- On kathavachak_master
ALTER TABLE kathavachak_master
ADD FULLTEXT INDEX idx_name (name);

-- On kathavachak_experties
ALTER TABLE kathavachak_experties
ADD FULLTEXT INDEX idx_cat_snap (cat_snap);

ALTER TABLE kathavachak_experties
ADD FULLTEXT INDEX idx_subcat_snap (subcat_snap);


INSERT INTO kathavachak_master
    (id, user_id, name, profile_picture_url, average_rating, total_ratings, is_active, created_at, updated_at)
VALUES
    (1, 'user-1234567890abcdef1234567890abcd', 'John Doe', 'https://example.com/profile/johndoe.jpg', 4.75, 150, TRUE, UTC_TIMESTAMP(), NULL);

    INSERT INTO kathavachak_experties
    (id, kathavachak_id, cat_id, subcat_id, yrs_of_exp, proficiency_level, cat_snap, subcat_snap)
VALUES
    (1, 1, 10, 100, 5, 'Expert', 'Storytelling', 'Folk Tales');

    INSERT INTO kathavachak_language
    (id, kathavachak_id, language_id)
VALUES
    (1, 1, 1);


    INSERT INTO kathavachak_topic
    (id, kathavachak_id, topic_name)
VALUES
    (1, 1, 'Mythology');

    INSERT INTO kathavachak_session_mode
    (id, kathavachak_id, mode_name)
VALUES
    (1, 1, 1);

    INSERT INTO kathavachak_schedule
    (id, kathavachak_id, start_date, end_date, is_available, is_recurring)
VALUES
    (1, 1, '2025-09-10', '2025-09-11', TRUE, true);

    INSERT INTO kathavachak_time_slot
    (id, kathavachak_id, start_time, end_time, day_of_week, is_booked)
VALUES
    (1, 1, '10:00:00', '11:00:00', 1, FALSE);

    INSERT INTO kathavachak_media
    (id, kathavachak_id, url, title, media_type, description)
VALUES
    (1, 1, 'https://example.com/media/storytelling_video.mp4', '', 1, '');

    INSERT INTO languages
    (id, language_code, language_name, display_order)
VALUES
    (1, 'en', 'English',1),
    (2, 'hi', 'Hindi',2),
    (3, 'mr', 'Marathi',3);
