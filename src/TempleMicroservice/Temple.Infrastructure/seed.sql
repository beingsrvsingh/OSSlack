use templedb;

-- ? FULLTEXT INDEXES FOR SEARCH OPTIMIZATION
ALTER TABLE temple_master
ADD FULLTEXT INDEX idx_name (name);

ALTER TABLE temple_expertise
ADD FULLTEXT INDEX idx_name (name);

ALTER TABLE temple_expertise
ADD FULLTEXT INDEX idx_cat_snap (category_name_snapshot);

ALTER TABLE temple_expertise
ADD FULLTEXT INDEX idx_subcat_snap (sub_category_name_snapshot);

INSERT INTO temple_master (
    id,
    location_id,
    name,
    description,
    thumbnail_url,
    is_active,
    created_at,
    updated_at
) VALUES
-- Temple 1
(1, 101, 'Shree Balaji Temple', 'Famous Balaji temple located in the city center.', 'https://cdn.temples.com/images/balaji.jpg', true, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP()),

-- Temple 2
(2, 102, 'Shree Ganesha Mandir', 'Ancient Ganesha temple with daily pooja and aarti.', 'https://cdn.temples.com/images/ganesha.jpg', true, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP()),

-- Temple 3
(3, 103, 'Durga Devi Temple', 'Navratri-special temple with devotional programs.', 'https://cdn.temples.com/images/durga.jpg', true, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP()),

-- Temple 4
(4, 104, 'Sai Baba Temple', 'Peaceful temple with meditation and prasad services.', 'https://cdn.temples.com/images/saibaba.jpg', true, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP());


INSERT INTO temple_expertise (
    id,
    temple_id,
    category_id,
    sub_category_id,
    category_name_snapshot,
    sub_category_name_snapshot,
    name,
    description,
    has_schedule,
    price,
    duration,
    average_rating,
    total_ratings,
    is_active,
    created_at,
    updated_at
) VALUES
-- Example Expertise 1: Daily Aarti
(1, 1, 10, 101, 'Aarti', 'Morning Aarti', 'Morning Aarti Service', 'Performed every day at sunrise.', TRUE, 0.00, '00:30:00', 4.8, 150, TRUE, CURRENT_TIMESTAMP(6), CURRENT_TIMESTAMP(6)),

-- Example Expertise 2: Special Pooja
(2, 1, 11, 201, 'Pooja', 'Ganesh Pooja', 'Ganesh Special Pooja', 'Performed on Ganesh Chaturthi.', TRUE, 500.00, '01:00:00', 4.9, 80, TRUE, CURRENT_TIMESTAMP(6), CURRENT_TIMESTAMP(6)),

-- Example Expertise 3: Prasad
(3, 1, 12, 301, 'Prasad', 'Ladoo Prasad', 'Ladoo Pack Prasad', '5 laddoos offered as prasad.', FALSE, 50.00, '00:00:00', 4.5, 200, TRUE, CURRENT_TIMESTAMP(6), CURRENT_TIMESTAMP(6)),

-- Example Expertise 4: Donation
(4, 1, 13, 401, 'Donation', 'General Donation', 'Support Temple Activities', NULL, FALSE, 100.00, '00:00:00', 0.00, 0, TRUE, CURRENT_TIMESTAMP(6), CURRENT_TIMESTAMP(6));


INSERT INTO attribute_value (
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
-- For Aarti
(1, 1, 1001, 5001, 'Sunrise', 'time_of_day', 'Time of Day', 1, NULL, CURRENT_TIMESTAMP(6)),

-- For Pooja
(2, 2, 1002, 5002, 'Ganesh Idol', 'main_deity', 'Main Deity', 2, NULL, CURRENT_TIMESTAMP(6)),

-- For Prasad
(3, 3, 1003, 5003, '5', 'quantity', 'Number of Items', 3, NULL, CURRENT_TIMESTAMP(6)),

-- For Donation
(4, 4, 1004, 5004, 'INR', 'currency', 'Currency Type', 2, NULL, CURRENT_TIMESTAMP(6));


INSERT INTO temple_schedule (
    id,
    temple_id,
    day_of_week,
    open_time,
    close_time,
    is_active,
    reason
) VALUES
(1, 1, 0, '06:00:00', '20:00:00', true, 'Regular hours'), -- Sunday
(2, 1, 1, '06:00:00', '20:00:00', true, 'Regular hours'), -- Monday
(3, 1, 2, '06:00:00', '20:00:00', true, 'Regular hours'), -- Tuesday
(4, 1, 3, '06:00:00', '20:00:00', true, 'Regular hours'), -- Wednesday
(5, 1, 4, '06:00:00', '20:00:00', true, 'Regular hours'), -- Thursday
(6, 1, 5, '06:00:00', '20:00:00', true, 'Regular hours'), -- Friday
(7, 1, 6, '06:00:00', '22:00:00', true, 'Extended hours'); -- Saturday


INSERT INTO temple_time_slot (
    id,
    schedule_id,
    slot_start_time,
    slot_end_time,
    max_capacity,
    booked_count,
    label,
    is_active,
    created_at
) VALUES
-- Sunday (schedule_id = 1)
(1, 1, '2025-09-14 07:00:00', '2025-09-14 07:30:00', 50, 10, 'Morning Aarti', true, CURRENT_TIMESTAMP(6)),
(2, 1, '2025-09-14 18:00:00', '2025-09-14 18:30:00', 50, 20, 'Evening Aarti', true, CURRENT_TIMESTAMP(6)),

-- Monday (schedule_id = 2)
(3, 2, '2025-09-15 07:00:00', '2025-09-15 07:30:00', 50, 15, 'Morning Aarti', true, CURRENT_TIMESTAMP(6)),
(4, 2, '2025-09-15 18:00:00', '2025-09-15 18:30:00', 50, 5, 'Evening Aarti', true, CURRENT_TIMESTAMP(6)),

-- Tuesday (schedule_id = 3)
(5, 3, '2025-09-16 07:00:00', '2025-09-16 07:30:00', 50, 25, 'Morning Aarti', true, CURRENT_TIMESTAMP(6)),
(6, 3, '2025-09-16 18:00:00', '2025-09-16 18:30:00', 50, 30, 'Evening Aarti', true, CURRENT_TIMESTAMP(6));


INSERT INTO temple_exception (
    id,
    temple_id,
    exception_date,
    is_closed,
    open_time,
    close_time,
    reason
) VALUES
(1, 1, '2025-09-14', true, NULL, NULL, 'Closed due to renovation');
