USE astrologerdb;

INSERT INTO astrologer_master 
(id, name, thumbnail_url, is_active, rating_snap, reviews_snap, 
 category_id, sub_category_id, category_name_snapshot, sub_category_name_snapshot, 
 currency, amount, mrp, tax, discount,
 is_trending, is_featured, created_at, updated_at)
VALUES
(1, 'AstroGuru', 'https://picsum.photos/seed/picsum/200/300', TRUE, 4.8, 250, 
 19, 24, 'Service', 'Astrologer', 
 'INR', 1500.00, 2000.00, 10.00, 0.00,
 TRUE, TRUE, NOW(), NOW()),

(2, 'DivineSeer', 'https://picsum.photos/seed/picsum/200/300', TRUE, 4.5, 180, 
 19, 24, 'Service', 'Astrologer',
 'INR', 1700.00, 2200.00, 12.00, 5.00,
 FALSE, TRUE, NOW(), NOW());


 INSERT INTO astrologer_expertise
(id, astrologer_id, name, amount, mrp, currency, tax, discount, 
 duration_minute, booking_type, is_default, stock_quantity, 
 price_effective_from, price_effective_to)
VALUES
(1, 1, 'Horoscope Reading', 1500.00, 2000.00, 'INR', 10.00, 0.00,
 30, 'Online', TRUE, 50, NOW(), NULL),

(2, 1, 'Palm Reading', 1200.00, 1600.00, 'INR', 8.00, 0.00,
 25, 'Online', TRUE, 40, NOW(), NULL),

(3, 2, 'Horoscope Reading', 1700.00, 2200.00, 'INR', 12.00, 5.00,
 35, 'Online', TRUE, 60, NOW(), NULL),

(4, 2, 'Palm Reading', 1300.00, 1700.00, 'INR', 9.00, 0.00,
 30, 'Online', TRUE, 55, NOW(), NULL);


 INSERT INTO astrologer_image 
(id, astrologer_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'AstroGuru Profile Photo', 1, NOW()),
(2, 2, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'DivineSeer Profile Photo', 1, NOW());


INSERT INTO astrologer_expertise_image 
(id, astrologer_expertise_id, media_type, image_url, alt_text, sort_order, created_at)
VALUES
(1, 1, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Horoscope Reading Session', 1, NOW()),
(2, 2, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Palm Reading Session', 1, NOW()),
(3, 3, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Horoscope Analysis', 1, NOW()),
(4, 4, 'Image', 'https://picsum.photos/seed/picsum/200/300', 'Palmistry Consultation', 1, NOW());


INSERT INTO astrologer_attribute_value
(id, expertise_id, catalog_attribute_id, catalog_attribute_value_id, 
 attribute_key, attribute_label, value, attribute_group_name_snap, created_at)
VALUES
(1, 1, 1, 1, 'Experience', 'Experience', '10 Years', 'Basic Info', NOW()),
(2, 1, 2, 2, 'ExpertiseLevel', 'Expertise Level', 'Advanced', 'Basic Info', NOW()),

(3, 2, 1, 3, 'Experience', 'Experience', '8 Years', 'Basic Info', NOW()),
(4, 2, 2, 4, 'ExpertiseLevel', 'Expertise Level', 'Intermediate', 'Basic Info', NOW()),

(5, 3, 1, 5, 'Experience', 'Experience', '12 Years', 'Basic Info', NOW()),
(6, 3, 2, 6, 'ExpertiseLevel', 'Expertise Level', 'Expert', 'Basic Info', NOW()),

(7, 4, 1, 7, 'Experience', 'Experience', '6 Years', 'Basic Info', NOW()),
(8, 4, 2, 8, 'ExpertiseLevel', 'Expertise Level', 'Intermediate', 'Basic Info', NOW());


INSERT INTO astrologer_addon 
(id, astrologer_id, astrologer_expertise_id, name, description, 
 amount, mrp, currency, tax, discount, 
 is_active, display_order, created_at, updated_at)
VALUES
(1, 1, 1, 'Astrology Report', 'Detailed astrology report PDF', 
 500.00, 600.00, 'INR', 5.00, 0.00,
 TRUE, 1, NOW(), NOW()),

(2, 1, 2, 'Lucky Gem Suggestion', 'Gemstone suitable for palm reading', 
 800.00, 950.00, 'INR', 8.00, 0.00,
 TRUE, 2, NOW(), NOW()),

(3, 2, 3, 'Numerology Chart', 'Comprehensive numerology report', 
 400.00, 500.00, 'INR', 5.00, 0.00,
 TRUE, 1, NOW(), NOW()),

(4, 2, 4, 'Crystal Recommendation', 'Crystal and healing stone suggestions', 
 600.00, 750.00, 'INR', 10.00, 0.00,
 TRUE, 2, NOW(), NOW());
