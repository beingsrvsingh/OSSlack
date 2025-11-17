use catalogdb;

SET FOREIGN_KEY_CHECKS = 0;
delete from subcategory_master where id is null;
SET FOREIGN_KEY_CHECKS = 0;

SELECT * FROM catalogdb.category_master;

INSERT INTO category_master 
(id, name, category_type, description, display_order, image_url, is_active, created_at, updated_at) 
VALUES
(1, 'Product', 'Product', 'Religious products and items', 1, 'https://example.com/images/product_icon.png', 1, NOW(), NOW()),
(2, 'Pooja', 'Pooja', 'Religious pooja services and rituals', 2, 'https://example.com/images/pooja_icon.png', 1, NOW(), NOW()),
(3, 'Temple', 'Temple', 'Religious temples and associated services', 3, 'https://example.com/images/temple_icon.png', 1, NOW(), NOW()),
(4, 'Priest', 'Priest', 'Priest services and rituals', 4, 'https://example.com/images/priest_icon.png', 1, NOW(), NOW()),
(5, 'Astrologer', 'Astrologer', 'Astrology services and products', 5, 'https://example.com/images/astrologer_icon.png', 1, NOW(), NOW()),
(6, 'Kathavachak', 'Kathavachak', 'Religious storytellers and discourses', 6, 'https://example.com/images/kathavachak_icon.png', 1, NOW(), NOW());



-- Category 3: Product (IDs 1 - 100)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(1, 1, 'Incense Sticks', 'Fragrant incense sticks used in temples and homes.', 1, 'Product', NULL, NOW(), NOW()),
(2, 1, 'Prayer Beads (Mala)', 'Beads used for meditation and chanting mantras.', 1, 'Product', NULL, NOW(), NOW()),
(3, 1, 'Holy Water Bottle', 'Bottled holy water from sacred temples.', 1, 'Product', NULL, NOW(), NOW()),
(4, 1, 'Camphor Tablets', 'Used in aarti and pooja rituals for lighting and fragrance.', 1, 'Product', NULL, NOW(), NOW()),
(5, 1, 'Dhoop Sticks', 'Special incense sticks for pooja with a rich fragrance.', 1, 'Product', NULL, NOW(), NOW()),
(6, 1, 'Pooja Thali Set', 'Complete pooja thali set including diya, bell, and more.', 1, 'Product', NULL, NOW(), NOW()),
(7, 1, 'Pooja Cloth', 'Special cloth for pooja altar or idols.', 1, 'Product', NULL, NOW(), NOW()),
(8, 1, 'Kumkum Powder', 'Red vermilion powder used for tilak and rituals.', 1, 'Product', NULL, NOW(), NOW()),
(9, 1, 'Haldi Powder', 'Turmeric powder used in pooja ceremonies.', 1, 'Product', NULL, NOW(), NOW()),
(10, 1, 'Rice Grains (Akshata)', 'Unbroken rice grains used during pooja rituals.', 1, 'Product', NULL, NOW(), NOW()),
(11, 1, 'Bell (Ghanti)', 'Used during aarti and chanting.', 1, 'Product', NULL, NOW(), NOW()),
(12, 1, 'Diya (Oil Lamp)', 'Traditional oil lamp used in pooja.', 1, 'Product', NULL, NOW(), NOW()),
(13, 1, 'Flowers for Pooja', 'Fresh or artificial flowers used for offerings.', 1, 'Product', NULL, NOW(), NOW()),
(14, 1, 'Camphor Stand', 'Holder for burning camphor during rituals.', 1, 'Product', NULL, NOW(), NOW()),
(15, 1, 'Sacred Thread (Mouli)', 'Thread used in pooja and religious ceremonies.', 1, 'Product', NULL, NOW(), NOW()),
(16, 1, 'Incense Holder', 'Holder for incense sticks during pooja.', 1, 'Product', NULL, NOW(), NOW()),
(17, 1, 'Pooja Samagri Box', 'Box containing assorted pooja essentials.', 1, 'Product', NULL, NOW(), NOW()),
(18, 1, 'Tantrik Yantra', 'Sacred tantric yantras for spiritual protection.', 1, 'Product', NULL, NOW(), NOW()),
(19, 1, 'Pooja Books', 'Books containing mantras and rituals.', 1, 'Product', NULL, NOW(), NOW()),
(20, 1, 'Ghee (Clarified Butter)', 'Used as fuel in lamps and havan.', 1, 'Product', NULL, NOW(), NOW()),
(21, 1, 'Panchamrit', 'Sacred mixture of milk, curd, honey, sugar, and ghee used in pooja.', 1, 'Product', NULL, NOW(), NOW()),
(22, 1, 'Betel Leaves', 'Used as offerings in many Hindu rituals.', 1, 'Product', NULL, NOW(), NOW()),
(23, 1, 'Coconut', 'Symbol of prosperity, used in rituals and offerings.', 1, 'Product', NULL, NOW(), NOW()),
(24, 1, 'Sindoor', 'Red powder used by married women and in pooja rituals.', 1, 'Product', NULL, NOW(), NOW()),
(25, 1, 'Jaggery', 'Used as sweet offering in pooja ceremonies.', 1, 'Product', NULL, NOW(), NOW()),
(26, 1, 'Ganga Jal', 'Holy water from the Ganges river.', 1, 'Product', NULL, NOW(), NOW()),
(27, 1, 'Havan Samagri', 'Mixture of herbs and materials used for fire rituals (havan).', 1, 'Product', NULL, NOW(), NOW()),
(28, 1, 'Moli Thread Pack', 'Pack of sacred threads for multiple poojas.', 1, 'Product', NULL, NOW(), NOW()),
(29, 1, 'Chandan (Sandalwood) Powder', 'Used for tilak and in pooja rituals.', 1, 'Product', NULL, NOW(), NOW()),
(30, 1, 'Flowers (Marigold)', 'Marigold flowers widely used in decoration and offerings.', 1, 'Product', NULL, NOW(), NOW()),
(31, 1, 'Ghee Diya Wicks', 'Cotton wicks used in oil lamps and diyas.', 1, 'Product', NULL, NOW(), NOW()),
(32, 1, 'Holy Ash (Vibhuti)', 'Sacred ash used in Shaivite rituals and tilak.', 1, 'Product', NULL, NOW(), NOW()),
(33, 1, 'Rice Flour', 'Used in making rangoli or ritual markings.', 1, 'Product', NULL, NOW(), NOW()),
(34, 1, 'Sacred Coins', 'Coins blessed for offering and rituals.', 1, 'Product', NULL, NOW(), NOW()),
(35, 1, 'Bell Metal Hand Bell', 'Used to ring during rituals to invoke deity presence.', 1, 'Product', NULL, NOW(), NOW()),
(36, 1, 'Tulsi Plant', 'Sacred Tulsi plant used in Hindu rituals and worship.', 1, 'Product', NULL, NOW(), NOW()),
(37, 1, 'Havan Kund', 'Fire pit used for performing havan (fire rituals).', 1, 'Product', NULL, NOW(), NOW()),
(38, 1, 'Kalash (Water Pot)', 'Sacred metal or clay pot used to hold holy water during pooja.', 1, 'Product', NULL, NOW(), NOW()),
(39, 1, 'Agarbatti Holder', 'Holder for incense sticks during rituals.', 1, 'Product', NULL, NOW(), NOW()),
(40, 1, 'Pooja Timer', 'Timer to keep track of ritual durations.', 1, 'Product', NULL, NOW(), NOW()),
(41, 1, 'Rudraksha Beads', 'Sacred beads used in meditation and pooja.', 1, 'Product', NULL, NOW(), NOW()),
(42, 1, 'Conch Shell (Shankh)', 'Blown during rituals and aartis to signify auspicious beginnings.', 1, 'Product', NULL, NOW(), NOW()),
(43, 1, 'Pooja Lantern', 'Decorative lanterns used to light the pooja altar.', 1, 'Product', NULL, NOW(), NOW()),
(44, 1, 'Yantra', 'Sacred geometric diagrams used as astrological remedies.', 1, 'Product', NULL, NOW(), NOW());


-- Category 1: Temple (IDs 101 - 200)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(101, 2, 'Aarti', 'Participate in temple aartis', 1, 'Temple Service', NULL, NOW(), NOW()),
(102, 2, 'Pooja', 'Book a ritual pooja', 1, 'Temple Service', NULL, NOW(), NOW()),
(103, 2, 'Prasad', 'Get prasad delivered', 1, 'Temple Service', NULL, NOW(), NOW()),
(104, 2, 'Donation', 'Donate to temple causes', 1, 'Temple Service', NULL, NOW(), NOW()),
(105, 2, 'VIP Pass', 'Priority entry options', 1, 'Temple Service', NULL, NOW(), NOW()),
(106, 2, 'Guide Tour', 'Guided temple experiences', 1, 'Temple Service', NULL, NOW(), NOW()),
(107, 2, 'All Temple Combo', 'Make your own combo booking', 1, 'Temple Service', NULL, NOW(), NOW()),
(108, 2, 'Festival Events', 'Participate in special temple festivals and seasonal celebrations', 1, 'Temple Service', NULL, NOW(), NOW()),
(109, 2, 'Temple Shop', 'Purchase religious items and souvenirs', 1, 'Temple Service', NULL, NOW(), NOW()),
(110, 2, 'Live Streaming', 'Watch live temple rituals and darshan online', 1, 'Temple Service', NULL, NOW(), NOW());


-- Category 2: Priest (IDs 201 - 300)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(201, 3, 'Temple Priest', 'Priests assigned to specific temples', 1, 'Priest Type', NULL, NOW(), NOW()),
(202, 3, 'Home Priest', 'Perform rituals at user’s home', 1, 'Priest Type', NULL, NOW(), NOW()),
(203, 3, 'Special Event', 'Specialized in weddings, naming, etc.', 1, 'Priest Type', NULL, NOW(), NOW()),
(204, 3, 'Pooja Specialist', 'Expert in particular deities/rituals', 1, 'Priest Type', NULL, NOW(), NOW()),
(205, 3, 'Spiritual Guide', 'Offers discourses, meditation, and guidance', 1, 'Priest Type', NULL, NOW(), NOW()),
(206, 3, 'Regional Specialist', 'Perform rituals in specific languages/regions', 1, 'Priest Type', NULL, NOW(), NOW()),
(207, 3, 'Quick Book Priest', 'Priests available on-demand or urgently', 1, 'Priest Type', NULL, NOW(), NOW()),
(208, 3, 'Online Rituals', 'Priests available via video call', 1, 'Priest Type', NULL, NOW(), NOW()),
(209, 3, 'Festival Priest', 'Focused on major festival rituals', 1, 'Priest Type', NULL, NOW(), NOW()),
(210, 3, 'Tantrik Priest', 'Priests specialized in Tantrik rituals and poojas', 1, 'Priest Type', NULL, NOW(), NOW());


-- Category 4: Astrologer (IDs 301 - 400)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(301, 4, 'Astrology Consultation', 'Personal horoscope reading and guidance from expert astrologers.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(302, 4, 'Birth Chart (Janam Kundali)', 'Detailed birth chart based on date, time, and place of birth.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(303, 4, 'Gemstone Recommendation', 'Astrologer recommended gemstones to balance planetary influences.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(304, 4, 'Yantra', 'Sacred geometric diagrams used as astrological remedies.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(305, 4, 'Palmistry Reading', 'Analysis of palm lines to predict future events.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(306, 4, 'Numerology Report', 'Personalized report based on numbers and dates.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(307, 4, 'Astrology Books', 'Books on astrology, planetary positions, and rituals.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(308, 4, 'Astrology Software Access', 'Access to advanced astrology calculation tools.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(309, 4, 'Horoscope Matching', 'Matchmaking services based on astrological compatibility.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(310, 4, 'Muhurta Selection', 'Choosing auspicious dates and times for important events.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(311, 4, 'Astrological Remedies', 'Yantras, gemstones, mantras and rituals for problems.', 1, 'Astrology Service', NULL, NOW(), NOW());

-- Category 5: Kathavachak Service (IDs 401 - 500)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(401, 5, 'Ramayana Kathavachak', 'Storytelling sessions of Ramayana', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(402, 5, 'Bhagavad Gita Kathavachak', 'Discourses and explanations of Bhagavad Gita', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(403, 5, 'Mahabharata Kathavachak', 'Narration of Mahabharata episodes', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(404, 5, 'Spiritual Discourses', 'General spiritual talks and teachings', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(405, 5, 'Online Kathavachak', 'Virtual storytelling sessions', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(406, 5, 'Onsite Katha Visit', 'Kathavachak visits your area for live storytelling events and discourses.', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(407, 5, 'Children Stories Kathavachak', 'Storytelling sessions specially for children', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(408, 5, 'Festival Special Kathavachak', 'Special katha sessions during festivals', 1, 'Kathavachak Service', NULL, NOW(), NOW());


-- Products 

INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
-- Incense Sticks (1)
(1001, 1, 'Sandalwood Incense', 'Sandalwood-scented incense sticks for calming atmosphere.', 1, 'Product', 1, NOW(), NOW()),
(1002, 1, 'Jasmine Incense', 'Jasmine-scented sticks ideal for evening aarti.', 1, 'Product', 1, NOW(), NOW()),
(1003, 1, 'Lavender Incense', 'Lavender incense for meditation and focus.', 1, 'Product', 1, NOW(), NOW()),

-- Prayer Beads (Mala) (2)
(1004, 1, 'Tulsi Mala', '108-bead Tulsi mala for chanting.', 1, 'Product', 2, NOW(), NOW()),
(1005, 1, 'Rudraksha Mala', 'Sacred Rudraksha beads for Lord Shiva devotees.', 1, 'Product', 2, NOW(), NOW()),
(1006, 1, 'Sphatik Mala', 'Crystal beads for positive energy.', 1, 'Product', 2, NOW(), NOW()),

-- Holy Water Bottle (3)
(1007, 1, 'Ganga Jal 250ml', 'Holy water from the Ganges.', 1, 'Product', 3, NOW(), NOW()),
(1008, 1, 'Yamuna Jal 250ml', 'Sacred Yamuna river water.', 1, 'Product', 3, NOW(), NOW()),
(1009, 1, 'Sangam Jal 500ml', 'Water from the confluence of rivers.', 1, 'Product', 3, NOW(), NOW()),

-- Camphor Tablets (4)
(1010, 1, 'Pure Bhimseni Camphor', 'High-quality camphor for aarti.', 1, 'Product', 4, NOW(), NOW()),
(1011, 1, 'Round Camphor Tablets', 'Standard camphor tablets for daily use.', 1, 'Product', 4, NOW(), NOW()),
(1012, 1, 'Camphor Cubes', 'Strong aromatic camphor cubes.', 1, 'Product', 4, NOW(), NOW()),

-- Dhoop Sticks (5)
(1013, 1, 'Loban Dhoop', 'Loban-scented dhoop sticks.', 1, 'Product', 5, NOW(), NOW()),
(1014, 1, 'Guggul Dhoop', 'Fragrant guggul dhoop for havan.', 1, 'Product', 5, NOW(), NOW()),
(1015, 1, 'Rose Dhoop', 'Rose-scented dhoop for divine fragrance.', 1, 'Product', 5, NOW(), NOW()),

-- Pooja Thali Set (6)
(1016, 1, 'Brass Pooja Thali', 'Traditional brass thali with diya and bell.', 1, 'Product', 6, NOW(), NOW()),
(1017, 1, 'Steel Pooja Thali', 'Durable stainless steel pooja thali.', 1, 'Product', 6, NOW(), NOW()),
(1018, 1, 'Decorative Thali Set', 'Beautifully decorated pooja thali set.', 1, 'Product', 6, NOW(), NOW()),

-- Pooja Cloth (7)
(1019, 1, 'Red Pooja Cloth', 'Soft red cloth used for deity altar.', 1, 'Product', 7, NOW(), NOW()),
(1020, 1, 'Yellow Cotton Cloth', 'Used for covering idols or thali.', 1, 'Product', 7, NOW(), NOW()),
(1021, 1, 'Silk Pooja Cloth', 'Elegant silk cloth for special rituals.', 1, 'Product', 7, NOW(), NOW()),

-- Kumkum Powder (8)
(1022, 1, 'Organic Kumkum', 'Natural red kumkum for tilak.', 1, 'Product', 8, NOW(), NOW()),
(1023, 1, 'Saffron Kumkum', 'Premium saffron-infused kumkum.', 1, 'Product', 8, NOW(), NOW()),
(1024, 1, 'Traditional Kumkum Pouch', 'Handmade kumkum in cloth pouch.', 1, 'Product', 8, NOW(), NOW()),

-- Haldi Powder (9)
(1025, 1, 'Organic Haldi', 'Pure turmeric for rituals.', 1, 'Product', 9, NOW(), NOW()),
(1026, 1, 'Temple Haldi Packet', 'Haldi specially packed for temples.', 1, 'Product', 9, NOW(), NOW()),
(1027, 1, 'Haldi-Kumkum Combo', 'Combo of haldi and kumkum.', 1, 'Product', 9, NOW(), NOW()),

-- Rice Grains (Akshata) (10)
(1028, 1, 'Plain Akshata', 'Unbroken rice used for blessings.', 1, 'Product', 10, NOW(), NOW()),
(1029, 1, 'Colored Akshata', 'Colored rice grains for festive rituals.', 1, 'Product', 10, NOW(), NOW()),
(1030, 1, 'Akshata with Turmeric', 'Rice mixed with haldi for puja.', 1, 'Product', 10, NOW(), NOW()),

-- Bell (Ghanti) (11)
(1031, 1, 'Brass Temple Bell', 'Traditional bell for aarti.', 1, 'Product', 11, NOW(), NOW()),
(1032, 1, 'Small Hand Bell', 'Handheld bell for pooja.', 1, 'Product', 11, NOW(), NOW()),
(1033, 1, 'Designer Ghanti', 'Decorative bell for home altar.', 1, 'Product', 11, NOW(), NOW()),

-- Diya (Oil Lamp) (12)
(1034, 1, 'Clay Diya Pack', 'Traditional diyas for Diwali.', 1, 'Product', 12, NOW(), NOW()),
(1035, 1, 'Brass Diya', 'Reusable diya with elegant finish.', 1, 'Product', 12, NOW(), NOW()),
(1036, 1, 'Floating Diya', 'Diya for placing in water bowls.', 1, 'Product', 12, NOW(), NOW()),

-- Flowers for Pooja (13)
(1037, 1, 'Fresh Marigold Garland', 'Garland used for deity decoration.', 1, 'Product', 13, NOW(), NOW()),
(1038, 1, 'Rose Petals', 'Fragrant petals for offering.', 1, 'Product', 13, NOW(), NOW()),
(1039, 1, 'Artificial Lotus', 'Reusable lotus flowers for altar.', 1, 'Product', 13, NOW(), NOW()),

-- Camphor Stand (14)
(1040, 1, 'Brass Camphor Holder', 'Traditional holder for burning camphor.', 1, 'Product', 14, NOW(), NOW()),
(1041, 1, 'Steel Camphor Plate', 'Simple steel stand for safe camphor use.', 1, 'Product', 14, NOW(), NOW()),
(1042, 1, 'Decorated Camphor Dish', 'Decorative stand for aarti rituals.', 1, 'Product', 14, NOW(), NOW()),

-- Sacred Thread (Mouli) (15)
(1043, 1, 'Red Sacred Thread', 'Traditional moli thread roll.', 1, 'Product', 15, NOW(), NOW()),
(1044, 1, 'Multicolor Moli', 'Colorful thread used in pooja.', 1, 'Product', 15, NOW(), NOW()),
(1045, 1, 'Cotton Moli Set', 'Pack of sacred threads.', 1, 'Product', 15, NOW(), NOW()),

-- Incense Holder (16)
(1046, 1, 'Wooden Incense Holder', 'Handcrafted wooden stand.', 1, 'Product', 16, NOW(), NOW()),
(1047, 1, 'Brass Incense Stand', 'Durable brass incense holder.', 1, 'Product', 16, NOW(), NOW()),
(1048, 1, 'Cone Incense Holder', 'Stand for incense cones.', 1, 'Product', 16, NOW(), NOW()),

-- Pooja Samagri Box (17)
(1049, 1, 'Basic Pooja Kit', 'Includes essential items for daily rituals.', 1, 'Product', 17, NOW(), NOW()),
(1050, 1, 'Festival Samagri Box', 'Special kit for major Hindu festivals.', 1, 'Product', 17, NOW(), NOW()),
(1051, 1, 'Customizable Samagri Pack', 'Select your own items for a custom kit.', 1, 'Product', 17, NOW(), NOW()),

-- Tantrik Yantra (18)
(1052, 1, 'Kali Yantra', 'For protection and spiritual strength.', 1, 'Product', 18, NOW(), NOW()),
(1053, 1, 'Baglamukhi Yantra', 'Used to remove enemies and obstacles.', 1, 'Product', 18, NOW(), NOW()),
(1054, 1, 'Sampurna Yantra Set', 'Complete set of powerful yantras.', 1, 'Product', 18, NOW(), NOW()),

-- Pooja Books (19)
(1055, 1, 'Daily Pooja Book', 'Contains simple mantras and prayers.', 1, 'Product', 19, NOW(), NOW()),
(1056, 1, 'Festival Ritual Guide', 'Detailed rituals for popular Hindu festivals.', 1, 'Product', 19, NOW(), NOW()),
(1057, 1, 'Devi Mahatmyam', 'Text of sacred Devi worship.', 1, 'Product', 19, NOW(), NOW()),

-- Ghee (20)
(1058, 1, 'Cow Ghee 250ml', 'Pure desi cow ghee for rituals.', 1, 'Product', 20, NOW(), NOW()),
(1059, 1, 'A2 Ghee 500ml', 'High-quality ghee made from A2 milk.', 1, 'Product', 20, NOW(), NOW()),
(1060, 1, 'Clarified Butter Tin', 'Bulk ghee for large ceremonies.', 1, 'Product', 20, NOW(), NOW()),


-- Panchamrit (21)
(1061, 1, 'Packaged Panchamrit', 'Ready-to-use mix for rituals.', 1, 'Product', 21, NOW(), NOW()),
(1062, 1, 'Fresh Panchamrit Mix', 'Perishable mix prepared for pooja.', 1, 'Product', 21, NOW(), NOW()),
(1063, 1, 'Panchamrit Kit', 'DIY ingredients for Panchamrit preparation.', 1, 'Product', 21, NOW(), NOW()),

-- Betel Leaves (22)
(1064, 1, 'Fresh Betel Leaves', 'Handpicked leaves for offerings.', 1, 'Product', 22, NOW(), NOW()),
(1065, 1, 'Betel Leaf Pack', 'Pack of 10 leaves for rituals.', 1, 'Product', 22, NOW(), NOW()),
(1066, 1, 'Organic Paan Leaves', 'Chemical-free leaves for pooja.', 1, 'Product', 22, NOW(), NOW()),

-- Coconut (23)
(1067, 1, 'Fresh Coconut', 'Whole coconut with water.', 1, 'Product', 23, NOW(), NOW()),
(1068, 1, 'Dry Coconut (Copra)', 'Used in havans and yagnas.', 1, 'Product', 23, NOW(), NOW()),
(1069, 1, 'Decorated Coconut', 'Wrapped in cloth for offerings.', 1, 'Product', 23, NOW(), NOW()),

-- Sindoor (24)
(1070, 1, 'Traditional Sindoor', 'Vermilion for ritual and marriage use.', 1, 'Product', 24, NOW(), NOW()),
(1071, 1, 'Organic Sindoor', 'Chemical-free sacred powder.', 1, 'Product', 24, NOW(), NOW()),
(1072, 1, 'Kumkum-Sindoor Combo', 'Combo pack for daily rituals.', 1, 'Product', 24, NOW(), NOW()),

-- Jaggery (25)
(1073, 1, 'Block Jaggery', 'Used in offerings and prasadam.', 1, 'Product', 25, NOW(), NOW()),
(1074, 1, 'Powdered Jaggery', 'Easy to mix in Panchamrit.', 1, 'Product', 25, NOW(), NOW()),
(1075, 1, 'Palm Jaggery', 'Traditional type from southern India.', 1, 'Product', 25, NOW(), NOW()),

-- Ganga Jal (26)
(1076, 1, 'Ganga Jal 100ml', 'Holy water from Haridwar.', 1, 'Product', 26, NOW(), NOW()),
(1077, 1, 'Ganga Jal Bottle 500ml', 'Used in all sacred rituals.', 1, 'Product', 26, NOW(), NOW()),
(1078, 1, 'Blessed Ganga Water', 'Sanctified at sacred ghats.', 1, 'Product', 26, NOW(), NOW()),

-- Havan Samagri (27)
(1079, 1, 'Basic Havan Kit', 'Includes herbs, ghee, and more.', 1, 'Product', 27, NOW(), NOW()),
(1080, 1, 'Premium Samagri Mix', 'High-quality items for big ceremonies.', 1, 'Product', 27, NOW(), NOW()),
(1081, 1, 'Mini Havan Pouch', 'For small home rituals.', 1, 'Product', 27, NOW(), NOW()),

-- Moli Thread Pack (28)
(1082, 1, 'Twin Moli Pack', 'Two moli rolls in one pack.', 1, 'Product', 28, NOW(), NOW()),
(1083, 1, 'Moli with Beads', 'Thread with decorative beads.', 1, 'Product', 28, NOW(), NOW()),
(1084, 1, 'Family Moli Pack', 'Set of molis for the whole family.', 1, 'Product', 28, NOW(), NOW()),

-- Chandan Powder (29)
(1085, 1, 'Sandalwood Chandan', 'Pure powder from sandalwood.', 1, 'Product', 29, NOW(), NOW()),
(1086, 1, 'Tilak Chandan Paste', 'Ready-to-use paste for tilak.', 1, 'Product', 29, NOW(), NOW()),
(1087, 1, 'Chandan Pack Combo', 'Powder and paste set.', 1, 'Product', 29, NOW(), NOW()),

-- Flowers (Marigold) (30)
(1088, 1, 'Fresh Marigold Bunch', 'Handpicked flowers.', 1, 'Product', 30, NOW(), NOW()),
(1089, 1, 'Artificial Marigold String', 'Reusable for altar decoration.', 1, 'Product', 30, NOW(), NOW()),
(1090, 1, 'Fragrant Marigold Petals', 'Used in aarti plates.', 1, 'Product', 30, NOW(), NOW()),

-- Ghee Diya Wicks (31)
(1091, 1, 'Cotton Wicks', 'For daily diya lighting.', 1, 'Product', 31, NOW(), NOW()),
(1092, 1, 'Pre-soaked Ghee Wicks', 'Ready to light wicks.', 1, 'Product', 31, NOW(), NOW()),
(1093, 1, 'Long Wicks Pack', 'Ideal for long-duration poojas.', 1, 'Product', 31, NOW(), NOW()),

-- Holy Ash (Vibhuti) (32)
(1094, 1, 'Sacred Vibhuti Pouch', 'Used for forehead tilak.', 1, 'Product', 32, NOW(), NOW()),
(1095, 1, 'Temple Vibhuti', 'From South Indian temples.', 1, 'Product', 32, NOW(), NOW()),
(1096, 1, 'Pure Herbal Ash', 'Prepared with holy herbs.', 1, 'Product', 32, NOW(), NOW()),

-- Rice Flour (33)
(1097, 1, 'White Rice Flour', 'Used for rangoli and rituals.', 1, 'Product', 33, NOW(), NOW()),
(1098, 1, 'Colored Rice Flour', 'For decorative rangoli.', 1, 'Product', 33, NOW(), NOW()),
(1099, 1, 'Fine Powdered Flour', 'Smooth texture for markings.', 1, 'Product', 33, NOW(), NOW()),

-- Sacred Coins (34)
(1100, 1, 'Gold Plated Sacred Coin', 'Blessed coin for offerings.', 1, 'Product', 34, NOW(), NOW()),
(1101, 1, 'Silver Sacred Coin', 'Silver coin used in pooja rituals.', 1, 'Product', 34, NOW(), NOW()),
(1102, 1, 'Copper Sacred Coin', 'Copper coin with religious engravings.', 1, 'Product', 34, NOW(), NOW()),

-- Bell Metal Hand Bell (35)
(1103, 1, 'Small Hand Bell', 'Used during aarti and rituals.', 1, 'Product', 35, NOW(), NOW()),
(1104, 1, 'Medium Hand Bell', 'Perfect size for pooja ceremonies.', 1, 'Product', 35, NOW(), NOW()),
(1105, 1, 'Large Bell with Handle', 'For temple use and special events.', 1, 'Product', 35, NOW(), NOW()),

-- Tulsi Plant (36)
(1106, 1, 'Potted Tulsi Plant', 'Sacred plant for home worship.', 1, 'Product', 36, NOW(), NOW()),
(1107, 1, 'Tulsi Plant Seeds', 'Grow your own holy plant.', 1, 'Product', 36, NOW(), NOW()),
(1108, 1, 'Tulsi Leaves Pack', 'Fresh leaves for pooja.', 1, 'Product', 36, NOW(), NOW()),

-- Havan Kund (37)
(1109, 1, 'Clay Havan Kund', 'Traditional fire pit for havan.', 1, 'Product', 37, NOW(), NOW()),
(1110, 1, 'Metal Havan Kund', 'Durable and reusable fire pit.', 1, 'Product', 37, NOW(), NOW()),
(1111, 1, 'Portable Havan Kund', 'Compact size for easy transport.', 1, 'Product', 37, NOW(), NOW()),

-- Kalash (Water Pot) (38)
(1112, 1, 'Brass Kalash', 'Used for storing holy water.', 1, 'Product', 38, NOW(), NOW()),
(1113, 1, 'Silver Plated Kalash', 'Elegant kalash for special poojas.', 1, 'Product', 38, NOW(), NOW()),
(1114, 1, 'Copper Kalash', 'Traditional water pot for rituals.', 1, 'Product', 38, NOW(), NOW()),

-- Agarbatti Holder (39)
(1115, 1, 'Wooden Agarbatti Holder', 'Decorative holder for incense sticks.', 1, 'Product', 39, NOW(), NOW()),
(1116, 1, 'Metal Incense Holder', 'Durable holder with intricate design.', 1, 'Product', 39, NOW(), NOW()),
(1117, 1, 'Stone Agarbatti Stand', 'Elegant stone holder for agarbatti.', 1, 'Product', 39, NOW(), NOW()),

-- Pooja Timer (40)
(1118, 1, 'Digital Pooja Timer', 'Tracks pooja duration accurately.', 1, 'Product', 40, NOW(), NOW()),
(1119, 1, 'Mechanical Hourglass Timer', 'Traditional sand timer for rituals.', 1, 'Product', 40, NOW(), NOW()),
(1120, 1, 'Compact Pooja Timer', 'Portable timer for on-the-go use.', 1, 'Product', 40, NOW(), NOW()),

-- Rudraksha Beads (41)
(1121, 1, '5 Mukhi Rudraksha', 'Blessed bead for spiritual benefits.', 1, 'Product', 41, NOW(), NOW()),
(1122, 1, '7 Mukhi Rudraksha', 'Known for healing properties.', 1, 'Product', 41, NOW(), NOW()),
(1123, 1, 'Mala Rudraksha Necklace', 'Complete rosary for chanting.', 1, 'Product', 41, NOW(), NOW()),

-- Conch Shell (Shankh) (42)
(1124, 1, 'Small Shankh', 'Used in daily poojas.', 1, 'Product', 42, NOW(), NOW()),
(1125, 1, 'Large Conch Shell', 'Blown during temple rituals.', 1, 'Product', 42, NOW(), NOW()),
(1126, 1, 'Decorated Shankh', 'Embellished with sacred symbols.', 1, 'Product', 42, NOW(), NOW()),

-- Pooja Lantern (43)
(1127, 1, 'Brass Pooja Lantern', 'Traditional lantern for pooja.', 1, 'Product', 43, NOW(), NOW()),
(1128, 1, 'Electric Pooja Lantern', 'Safe and reusable lantern.', 1, 'Product', 43, NOW(), NOW()),
(1129, 1, 'Glass Pooja Lantern', 'Decorative and durable lantern.', 1, 'Product', 43, NOW(), NOW());


-- Temple 

INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
-- Children of Parent Subcategory 101 (IDs 2001-2003)
(2001, 2, 'Subcategory 1 of 101', 'Description for subcategory 1 under parent 101', 1, 'Category 2 Item', 101, NOW(), NOW()),
(2002, 2, 'Subcategory 2 of 101', 'Description for subcategory 2 under parent 101', 1, 'Category 2 Item', 101, NOW(), NOW()),
(2003, 2, 'Subcategory 3 of 101', 'Description for subcategory 3 under parent 101', 1, 'Category 2 Item', 101, NOW(), NOW()),

-- Children of Parent Subcategory 102 (IDs 2004-2006)
(2004, 2, 'Subcategory 1 of 102', 'Description for subcategory 1 under parent 102', 1, 'Category 2 Item', 102, NOW(), NOW()),
(2005, 2, 'Subcategory 2 of 102', 'Description for subcategory 2 under parent 102', 1, 'Category 2 Item', 102, NOW(), NOW()),
(2006, 2, 'Subcategory 3 of 102', 'Description for subcategory 3 under parent 102', 1, 'Category 2 Item', 102, NOW(), NOW()),

-- Children of Parent Subcategory 103 (IDs 2007-2009)
(2007, 2, 'Subcategory 1 of 103', 'Description for subcategory 1 under parent 103', 1, 'Category 2 Item', 103, NOW(), NOW()),
(2008, 2, 'Subcategory 2 of 103', 'Description for subcategory 2 under parent 103', 1, 'Category 2 Item', 103, NOW(), NOW()),
(2009, 2, 'Subcategory 3 of 103', 'Description for subcategory 3 under parent 103', 1, 'Category 2 Item', 103, NOW(), NOW()),

-- Children of Parent Subcategory 104 (IDs 2010-2012)
(2010, 2, 'Subcategory 1 of 104', 'Description for subcategory 1 under parent 104', 1, 'Category 2 Item', 104, NOW(), NOW()),
(2011, 2, 'Subcategory 2 of 104', 'Description for subcategory 2 under parent 104', 1, 'Category 2 Item', 104, NOW(), NOW()),
(2012, 2, 'Subcategory 3 of 104', 'Description for subcategory 3 under parent 104', 1, 'Category 2 Item', 104, NOW(), NOW()),

-- Children of Parent Subcategory 105 (IDs 2013-2015)
(2013, 2, 'Subcategory 1 of 105', 'Description for subcategory 1 under parent 105', 1, 'Category 2 Item', 105, NOW(), NOW()),
(2014, 2, 'Subcategory 2 of 105', 'Description for subcategory 2 under parent 105', 1, 'Category 2 Item', 105, NOW(), NOW()),
(2015, 2, 'Subcategory 3 of 105', 'Description for subcategory 3 under parent 105', 1, 'Category 2 Item', 105, NOW(), NOW()),

-- Children of Parent Subcategory 106 (IDs 2016-2018)
(2016, 2, 'Subcategory 1 of 106', 'Description for subcategory 1 under parent 106', 1, 'Category 2 Item', 106, NOW(), NOW()),
(2017, 2, 'Subcategory 2 of 106', 'Description for subcategory 2 under parent 106', 1, 'Category 2 Item', 106, NOW(), NOW()),
(2018, 2, 'Subcategory 3 of 106', 'Description for subcategory 3 under parent 106', 1, 'Category 2 Item', 106, NOW(), NOW()),

-- Children of Parent Subcategory 107 (IDs 2019-2021)
(2019, 2, 'Subcategory 1 of 107', 'Description for subcategory 1 under parent 107', 1, 'Category 2 Item', 107, NOW(), NOW()),
(2020, 2, 'Subcategory 2 of 107', 'Description for subcategory 2 under parent 107', 1, 'Category 2 Item', 107, NOW(), NOW()),
(2021, 2, 'Subcategory 3 of 107', 'Description for subcategory 3 under parent 107', 1, 'Category 2 Item', 107, NOW(), NOW()),

-- Children of Parent Subcategory 108 (IDs 2022-2024)
(2022, 2, 'Subcategory 1 of 108', 'Description for subcategory 1 under parent 108', 1, 'Category 2 Item', 108, NOW(), NOW()),
(2023, 2, 'Subcategory 2 of 108', 'Description for subcategory 2 under parent 108', 1, 'Category 2 Item', 108, NOW(), NOW()),
(2024, 2, 'Subcategory 3 of 108', 'Description for subcategory 3 under parent 108', 1, 'Category 2 Item', 108, NOW(), NOW()),

-- Children of Parent Subcategory 109 (IDs 2025-2027)
(2025, 2, 'Subcategory 1 of 109', 'Description for subcategory 1 under parent 109', 1, 'Category 2 Item', 109, NOW(), NOW()),
(2026, 2, 'Subcategory 2 of 109', 'Description for subcategory 2 under parent 109', 1, 'Category 2 Item', 109, NOW(), NOW()),
(2027, 2, 'Subcategory 3 of 109', 'Description for subcategory 3 under parent 109', 1, 'Category 2 Item', 109, NOW(), NOW()),

-- Children of Parent Subcategory 110 (IDs 2028-2030)
(2028, 2, 'Subcategory 1 of 110', 'Description for subcategory 1 under parent 110', 1, 'Category 2 Item', 110, NOW(), NOW()),
(2029, 2, 'Subcategory 2 of 110', 'Description for subcategory 2 under parent 110', 1, 'Category 2 Item', 110, NOW(), NOW()),
(2030, 2, 'Subcategory 3 of 110', 'Description for subcategory 3 under parent 110', 1, 'Category 2 Item', 110, NOW(), NOW());


-- Priest

INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
-- Children of Parent Subcategory 201 (3001-3010)
(3001, 3, 'Product Specialist 1', 'Expert in category 201 products and services', 1, 'Product Specialist Role', 201, NOW(), NOW()),
(3002, 3, 'Product Specialist 2', 'Handles customer queries related to 201', 1, 'Product Specialist Role', 201, NOW(), NOW()),
(3003, 3, 'Product Specialist 3', 'Manages inventory and quality for 201', 1, 'Product Specialist Role', 201, NOW(), NOW()),

-- Children of Parent Subcategory 202 (3011-3020)
(3011, 3, 'Service Specialist 1', 'Expert in category 202 services', 1, 'Service Specialist Role', 202, NOW(), NOW()),
(3012, 3, 'Service Specialist 2', 'Customer service and support for 202', 1, 'Service Specialist Role', 202, NOW(), NOW()),
(3013, 3, 'Service Specialist 3', 'Quality assurance for 202 services', 1, 'Service Specialist Role', 202, NOW(), NOW()),

-- Children of Parent Subcategory 203 (3021-3030)
(3021, 3, 'Consultant 1', 'Provides consultancy for category 203', 1, 'Consultant Role', 203, NOW(), NOW()),
(3022, 3, 'Consultant 2', 'Handles client engagements for 203', 1, 'Consultant Role', 203, NOW(), NOW()),
(3023, 3, 'Consultant 3', 'Supports project execution for 203', 1, 'Consultant Role', 203, NOW(), NOW()),

-- Children of Parent Subcategory 204 (3031-3040)
(3031, 3, 'Advisor 1', 'Advisor for category 204 products', 1, 'Advisor Role', 204, NOW(), NOW()),
(3032, 3, 'Advisor 2', 'Advises customers on 204', 1, 'Advisor Role', 204, NOW(), NOW()),
(3033, 3, 'Advisor 3', 'Supports advisory services for 204', 1, 'Advisor Role', 204, NOW(), NOW()),

-- Children of Parent Subcategory 205 (3041-3050)
(3041, 3, 'Trainer 1', 'Trainer for category 205 product usage', 1, 'Trainer Role', 205, NOW(), NOW()),
(3042, 3, 'Trainer 2', 'Conducts training sessions for 205', 1, 'Trainer Role', 205, NOW(), NOW()),
(3043, 3, 'Trainer 3', 'Develops training material for 205', 1, 'Trainer Role', 205, NOW(), NOW());


-- Astrology 

INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
-- Children for 301 Astrology Consultation
(4001, 4, 'Basic Horoscope Reading', 'General personality and future insights.', 1, 'Astrology Child Item', 301, NOW(), NOW()),
(4002, 4, 'Detailed Horoscope Report', 'In-depth analysis with remedies.', 1, 'Astrology Child Item', 301, NOW(), NOW()),
(4003, 4, 'Annual Predictions', 'Yearly astrology forecast.', 1, 'Astrology Child Item', 301, NOW(), NOW()),

-- Children for 302 Birth Chart (Janam Kundali)
(4004, 4, 'Basic Birth Chart', 'Basic natal chart with planetary positions.', 1, 'Astrology Child Item', 302, NOW(), NOW()),
(4005, 4, 'Detailed Janam Kundali Report', 'Includes dashas and transits.', 1, 'Astrology Child Item', 302, NOW(), NOW()),
(4006, 4, 'Birth Chart Matching for Marriage', 'Compatibility report based on kundali.', 1, 'Astrology Child Item', 302, NOW(), NOW()),

-- Children for 303 Gemstone Recommendation
(4007, 4, 'Gemstone Consultation', 'Gemstone recommendations based on horoscope.', 1, 'Astrology Child Item', 303, NOW(), NOW()),
(4008, 4, 'Certified Gemstone Set', 'Set of gemstones for astrological benefits.', 1, 'Astrology Child Item', 303, NOW(), NOW()),
(4009, 4, 'Gemstone Care Guide', 'Instructions on wearing and maintaining gemstones.', 1, 'Astrology Child Item', 303, NOW(), NOW()),

-- Children for 304 Yantra
(4010, 4, 'Basic Yantra', 'Single purpose yantra for positivity.', 1, 'Astrology Child Item', 304, NOW(), NOW()),
(4011, 4, 'Complete Yantra Kit', 'Multiple yantras for protection and prosperity.', 1, 'Astrology Child Item', 304, NOW(), NOW()),
(4012, 4, 'Yantra Energization Service', 'Blessing and energizing yantras by priests.', 1, 'Astrology Child Item', 304, NOW(), NOW()),

-- Children for 305 Palmistry Reading
(4013, 4, 'Basic Palm Reading', 'General palm analysis.', 1, 'Astrology Child Item', 305, NOW(), NOW()),
(4014, 4, 'Detailed Palmistry Report', 'Includes health, career and love predictions.', 1, 'Astrology Child Item', 305, NOW(), NOW()),
(4015, 4, 'Palmistry with Face Reading', 'Combined analysis of palm and face.', 1, 'Astrology Child Item', 305, NOW(), NOW()),

-- Children for 306 Numerology Report
(4016, 4, 'Basic Numerology Chart', 'Includes life path and destiny numbers.', 1, 'Astrology Child Item', 306, NOW(), NOW()),
(4017, 4, 'Detailed Numerology Analysis', 'Comprehensive report with predictions.', 1, 'Astrology Child Item', 306, NOW(), NOW()),
(4018, 4, 'Name Change Suggestions', 'Numerological recommendations for name change.', 1, 'Astrology Child Item', 306, NOW(), NOW()),

-- Children for 307 Astrology Books
(4019, 4, 'Introduction to Astrology', 'Beginner-friendly astrology book.', 1, 'Astrology Child Item', 307, NOW(), NOW()),
(4020, 4, 'Advanced Horoscope Analysis', 'Book for experienced astrology readers.', 1, 'Astrology Child Item', 307, NOW(), NOW()),
(4021, 4, 'Palmistry and Numerology Guide', 'Combined study book.', 1, 'Astrology Child Item', 307, NOW(), NOW()),

-- Children for 308 Astrology Software Access
(4022, 4, 'Basic Software Access', 'Limited astrology calculations.', 1, 'Astrology Child Item', 308, NOW(), NOW()),
(4023, 4, 'Premium Software Access', 'Full features including kundali, dashas, and remedies.', 1, 'Astrology Child Item', 308, NOW(), NOW()),
(4024, 4, 'Mobile App Access', 'Astrology calculations on the go.', 1, 'Astrology Child Item', 308, NOW(), NOW()),

-- Children for 309 Horoscope Matching
(4025, 4, 'Marriage Compatibility Report', 'Detailed kundali matching for marriage.', 1, 'Astrology Child Item', 309, NOW(), NOW()),
(4026, 4, 'Business Partner Matching', 'Compatibility for business partnerships.', 1, 'Astrology Child Item', 309, NOW(), NOW()),
(4027, 4, 'Friendship Compatibility', 'Astrological report for friendship.', 1, 'Astrology Child Item', 309, NOW(), NOW()),

-- Children for 310 Muhurta Selection
(4028, 4, 'Wedding Muhurta', 'Auspicious dates and times for weddings.', 1, 'Astrology Child Item', 310, NOW(), NOW()),
(4029, 4, 'Housewarming Muhurta', 'Best time for moving into new home.', 1, 'Astrology Child Item', 310, NOW(), NOW()),
(4030, 4, 'Business Launch Muhurta', 'Timing for starting new ventures.', 1, 'Astrology Child Item', 310, NOW(), NOW()),

-- Children for 311 Astrological Remedies
(4031, 4, 'Mantra Chanting Service', 'Customized mantras for remedies.', 1, 'Astrology Child Item', 311, NOW(), NOW()),
(4032, 4, 'Rudraksha Beads Set', 'Set of beads for spiritual benefits.', 1, 'Astrology Child Item', 311, NOW(), NOW()),
(4033, 4, 'Pooja & Havan Service', 'Specialized poojas for planetary remedies.', 1, 'Astrology Child Item', 311, NOW(), NOW());

-- Kathavachak 

INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
-- Children for Ramayana Kathavachak (401)
(5001, 5, 'Ramayana Chapter 1', 'Storytelling of Ramayana Chapter 1', 1, 'Kathavachak Service Item', 401, NOW(), NOW()),
(5002, 5, 'Ramayana Chapter 2', 'Storytelling of Ramayana Chapter 2', 1, 'Kathavachak Service Item', 401, NOW(), NOW()),
(5003, 5, 'Ramayana Chapter 3', 'Storytelling of Ramayana Chapter 3', 1, 'Kathavachak Service Item', 401, NOW(), NOW()),

-- Children for Bhagavad Gita Kathavachak (402)
(5004, 5, 'Bhagavad Gita Chapter 1', 'Explanation of Bhagavad Gita Chapter 1', 1, 'Kathavachak Service Item', 402, NOW(), NOW()),
(5005, 5, 'Bhagavad Gita Chapter 2', 'Explanation of Bhagavad Gita Chapter 2', 1, 'Kathavachak Service Item', 402, NOW(), NOW()),
(5006, 5, 'Bhagavad Gita Chapter 3', 'Explanation of Bhagavad Gita Chapter 3', 1, 'Kathavachak Service Item', 402, NOW(), NOW()),

-- Children for Mahabharata Kathavachak (403)
(5007, 5, 'Mahabharata Episode 1', 'Narration of Mahabharata Episode 1', 1, 'Kathavachak Service Item', 403, NOW(), NOW()),
(5008, 5, 'Mahabharata Episode 2', 'Narration of Mahabharata Episode 2', 1, 'Kathavachak Service Item', 403, NOW(), NOW()),
(5009, 5, 'Mahabharata Episode 3', 'Narration of Mahabharata Episode 3', 1, 'Kathavachak Service Item', 403, NOW(), NOW()),

-- Children for Spiritual Discourses (404)
(5010, 5, 'Spiritual Talk 1', 'General spiritual discourse session 1', 1, 'Kathavachak Service Item', 404, NOW(), NOW()),
(5011, 5, 'Spiritual Talk 2', 'General spiritual discourse session 2', 1, 'Kathavachak Service Item', 404, NOW(), NOW()),
(5012, 5, 'Spiritual Talk 3', 'General spiritual discourse session 3', 1, 'Kathavachak Service Item', 404, NOW(), NOW()),

-- Children for Online Kathavachak (405)
(5013, 5, 'Online Session 1', 'Virtual storytelling session 1', 1, 'Kathavachak Service Item', 405, NOW(), NOW()),
(5014, 5, 'Online Session 2', 'Virtual storytelling session 2', 1, 'Kathavachak Service Item', 405, NOW(), NOW()),
(5015, 5, 'Online Session 3', 'Virtual storytelling session 3', 1, 'Kathavachak Service Item', 405, NOW(), NOW()),

-- Children for Onsite Katha Visit (406)
(5016, 5, 'Live Event 1', 'Onsite live storytelling event 1', 1, 'Kathavachak Service Item', 406, NOW(), NOW()),
(5017, 5, 'Live Event 2', 'Onsite live storytelling event 2', 1, 'Kathavachak Service Item', 406, NOW(), NOW()),
(5018, 5, 'Live Event 3', 'Onsite live storytelling event 3', 1, 'Kathavachak Service Item', 406, NOW(), NOW()),

-- Children for Children Stories Kathavachak (407)
(5019, 5, 'Bedtime Stories', 'Storytelling sessions for children before bedtime', 1, 'Kathavachak Service Item', 407, NOW(), NOW()),
(5020, 5, 'Moral Stories', 'Stories teaching morals and values', 1, 'Kathavachak Service Item', 407, NOW(), NOW()),
(5021, 5, 'Interactive Storytelling', 'Engaging children through interaction', 1, 'Kathavachak Service Item', 407, NOW(), NOW()),

-- Children for Festival Special Kathavachak (408)
(5022, 5, 'Diwali Katha', 'Special stories for Diwali festival', 1, 'Kathavachak Service Item', 408, NOW(), NOW()),
(5023, 5, 'Navratri Katha', 'Special katha sessions during Navratri', 1, 'Kathavachak Service Item', 408, NOW(), NOW()),
(5024, 5, 'Janmashtami Katha', 'Stories celebrating Krishna’s birth', 1, 'Kathavachak Service Item', 408, NOW(), NOW());


-----------------------------------------------------------------------------------------------------------------------------------------------------

use catalogdb;


INSERT INTO catalog_attribute (
    id,
    catalog_attribute_key,
    category_id,
    sub_category_id,
    attribute_datatype_id,
    label,
    attribute_group_id,
    is_custom,
    is_required,
    is_filterable,
    is_summary,
    sort_order,
    attribute_icon_id,
    created_at,
    updated_at,
    allowed_values_source
) VALUES
-- Basic attributes common to many products
(1, 'brand', 1, NULL, 1, 'Brand', 1, 0, 0, 1, 1, 1, NULL, NOW(), NOW(), 'brands'),
(2, 'price', 1, NULL, 4, 'Price', 1, 1, 1, 1, 1, 2, NULL, NOW(), NOW(), NULL),
(3, 'pack_size', 1, NULL, 3, 'Pack Size', 1, 0, 0, 1, 1, 3, NULL, NOW(), NOW(), NULL),
(4, 'weight', 1, NULL, 3, 'Weight (grams)', 1, 0, 0, 1, 1, 4, NULL, NOW(), NOW(), NULL),
(5, 'color', 1, NULL, 2, 'Color', 1, 0, 0, 1, 1, 5, NULL, NOW(), NOW(), 'colors'),

-- Fragrance related (important for incense/dhoop sticks)
(6, 'fragrance', 1, NULL, 2, 'Fragrance', 2, 0, 0, 1, 1, 6, NULL, NOW(), NOW(), 'fragrances'),

-- Material (wood, herbal, synthetic, etc)
(7, 'material', 1, NULL, 2, 'Material', 2, 0, 0, 1, 1, 7, NULL, NOW(), NOW(), 'materials'),

-- Usage / purpose (temple, home, meditation)
(8, 'usage', 1, NULL, 2, 'Usage', 2, 0, 0, 1, 1, 8, NULL, NOW(), NOW(), 'usage_types'),

-- Duration (burn time for sticks or candles)
(9, 'duration', 1, NULL, 3, 'Burn Time (minutes)', 3, 0, 0, 1, 1, 9, NULL, NOW(), NOW(), NULL),

-- Ingredients / composition
(10, 'ingredients', 1, NULL, 5, 'Ingredients', 3, 0, 0, 0, 0, 10, NULL, NOW(), NOW(), NULL),

-- Certification or authenticity marks
(11, 'certification', 1, NULL, 2, 'Certification', 4, 0, 0, 1, 1, 11, NULL, NOW(), NOW(), 'certifications'),

-- Packaging type
(12, 'packaging_type', 1, NULL, 2, 'Packaging Type', 4, 0, 0, 1, 1, 12, NULL, NOW(), NOW(), 'packaging_types'),

-- Country of origin
(13, 'country_of_origin', 1, NULL, 2, 'Country of Origin', 4, 0, 0, 1, 1, 13, NULL, NOW(), NOW(), 'countries'),

-- User ratings (numeric)
(14, 'user_rating', 1, NULL, 4, 'User Rating', 5, 0, 0, 1, 1, 14, NULL, NOW(), NOW(), NULL),

-- Warranty / shelf life
(15, 'shelf_life', 1, NULL, 2, 'Shelf Life', 5, 0, 0, 0, 0, 15, NULL, NOW(), NOW(), NULL),

-- Safety info (flammability etc)
(16, 'safety_info', 1, NULL, 5, 'Safety Information', 6, 0, 0, 0, 0, 16, NULL, NOW(), NOW(), NULL),

-- Manufacturer details
(17, 'manufacturer', 1, NULL, 2, 'Manufacturer', 6, 0, 0, 0, 0, 17, NULL, NOW(), NOW(), NULL),

-- Popularity (sort by)
(18, 'popularity', 1, NULL, 4, 'Popularity', 5, 0, 0, 0, 0, 18, NULL, NOW(), NOW(), NULL),

-- Stock availability
(19, 'stock_status', 1, NULL, 2, 'Stock Status', 1, 0, 0, 1, 1, 19, NULL, NOW(), NOW(), 'stock_status'),

-- Dimensions (height, width, depth)
(20, 'dimensions', 1, NULL, 5, 'Dimensions', 3, 0, 0, 0, 0, 20, NULL, NOW(), NOW(), NULL),

-- Additional attributes specific to incense sticks or pooja items
(21, 'stick_length', 1, NULL, 3, 'Stick Length (cm)', 3, 0, 0, 0, 0, 21, NULL, NOW(), NOW(), NULL),
(22, 'stick_thickness', 1, NULL, 3, 'Stick Thickness (mm)', 3, 0, 0, 0, 0, 22, NULL, NOW(), NOW(), NULL),
(23, 'smoke_intensity', 1, NULL, 3, 'Smoke Intensity', 3, 0, 0, 1, 1, 23, NULL, NOW(), NOW(), NULL),
(24, 'fragrance_type', 1, NULL, 2, 'Fragrance Type', 2, 0, 0, 1, 1, 24, NULL, NOW(), NOW(), 'fragrance_types'),

-- For beads and malas
(25, 'bead_material', 1, NULL, 2, 'Bead Material', 2, 0, 0, 1, 1, 25, NULL, NOW(), NOW(), 'bead_materials'),
(26, 'bead_size', 1, NULL, 3, 'Bead Size (mm)', 3, 0, 0, 1, 1, 26, NULL, NOW(), NOW(), NULL),
(27, 'bead_count', 1, NULL, 3, 'Bead Count', 3, 0, 0, 1, 1, 27, NULL, NOW(), NOW(), NULL),

-- For holy water and liquids
(28, 'volume', 1, NULL, 3, 'Volume (ml)', 3, 0, 0, 1, 1, 28, NULL, NOW(), NOW(), NULL),
(29, 'source_location', 1, NULL, 2, 'Source Location', 4, 0, 0, 1, 1, 29, NULL, NOW(), NOW(), NULL),

-- For thalis and pooja kits
(30, 'material_finish', 1, NULL, 2, 'Material Finish', 2, 0, 0, 1, 1, 30, NULL, NOW(), NOW(), 'material_finishes'),
(31, 'number_of_items', 1, NULL, 3, 'Number of Items', 3, 0, 0, 1, 1, 31, NULL, NOW(), NOW(), NULL),

-- For powders (kumkum, haldi etc)
(32, 'powder_grain_size', 1, NULL, 2, 'Powder Grain Size', 2, 0, 0, 1, 1, 32, NULL, NOW(), NOW(), 'grain_sizes'),

-- For ghee, oils etc
(33, 'purity_percentage', 1, NULL, 3, 'Purity Percentage', 3, 0, 0, 1, 1, 33, NULL, NOW(), NOW(), NULL),

-- Ritual specific (yantras, rudraksha etc)
(34, 'ritual_significance', 1, NULL, 5, 'Ritual Significance', 6, 0, 0, 0, 0, 34, NULL, NOW(), NOW(), NULL),

-- Product certification or traditional recognition
(35, 'traditional_certification', 1, NULL, 2, 'Traditional Certification', 4, 0, 0, 1, 1, 35, NULL, NOW(), NOW(), 'traditional_certs'),

-- For cloths
(36, 'fabric_type', 1, NULL, 2, 'Fabric Type', 2, 0, 0, 1, 1, 36, NULL, NOW(), NOW(), 'fabric_types'),

-- For bells, diyas, metal items
(37, 'metal_type', 1, NULL, 2, 'Metal Type', 2, 0, 0, 1, 1, 37, NULL, NOW(), NOW(), 'metal_types'),
(38, 'weight_metal', 1, NULL, 3, 'Metal Weight (grams)', 3, 0, 0, 0, 0, 38, NULL, NOW(), NOW(), NULL),

-- For flowers
(39, 'flower_type', 1, NULL, 2, 'Flower Type', 2, 0, 0, 1, 1, 39, NULL, NOW(), NOW(), 'flower_types'),

-- User Experience (UX) and Marketing attributes
(40, 'best_seller', 1, NULL, 1, 'Best Seller', 5, 0, 0, 1, 1, 40, NULL, NOW(), NOW(), NULL),
(41, 'new_arrival', 1, NULL, 1, 'New Arrival', 5, 0, 0, 1, 1, 41, NULL, NOW(), NOW(), NULL),

-- For timer or gadget products
(42, 'timer_duration', 1, NULL, 3, 'Timer Duration (minutes)', 3, 0, 0, 0, 0, 42, NULL, NOW(), NOW(), NULL),

-- Miscellaneous attributes for detailed description
(43, 'usage_instructions', 1, NULL, 5, 'Usage Instructions', 6, 0, 0, 0, 0, 43, NULL, NOW(), NOW(), NULL),
(44, 'storage_instructions', 1, NULL, 5, 'Storage Instructions', 6, 0, 0, 0, 0, 44, NULL, NOW(), NOW(), NULL),

-- For handmade or artisan products
(45, 'handmade', 1, NULL, 1, 'Handmade', 5, 0, 0, 1, 1, 45, NULL, NOW(), NOW(), NULL),

-- Environmental friendly attributes
(46, 'eco_friendly', 1, NULL, 1, 'Eco Friendly', 5, 0, 0, 1, 1, 46, NULL, NOW(), NOW(), NULL),

-- For sacred items
(47, 'blessed_by', 1, NULL, 2, 'Blessed By', 6, 0, 0, 0, 0, 47, NULL, NOW(), NOW(), NULL),

-- Expiry date
(48, 'expiry_date', 1, NULL, 6, 'Expiry Date', 6, 0, 0, 0, 0, 48, NULL, NOW(), NOW(), NULL),

-- Manufacturing date
(49, 'manufacturing_date', 1, NULL, 6, 'Manufacturing Date', 6, 0, 0, 0, 0, 49, NULL, NOW(), NOW(), NULL),

-- Warranty period
(50, 'warranty_period', 1, NULL, 2, 'Warranty Period', 6, 0, 0, 0, 0, 50, NULL, NOW(), NOW(), NULL);




INSERT INTO attribute_allowed_value (id, attribute_id, value, sort_order, created_at) VALUES
-- brands for attribute_id = 1 (brand)
(1, 1, 'Aroma Bliss', 1, NOW()),
(2, 1, 'Divine Scents', 2, NOW()),
(3, 1, 'Holy Fragrance', 3, NOW()),

-- colors for attribute_id = 5 (color)
(4, 5, 'Red', 1, NOW()),
(5, 5, 'Yellow', 2, NOW()),
(6, 5, 'Green', 3, NOW()),
(7, 5, 'Blue', 4, NOW()),

-- fragrances for attribute_id = 6 (fragrance)
(8, 6, 'Sandalwood', 1, NOW()),
(9, 6, 'Jasmine', 2, NOW()),
(10, 6, 'Rose', 3, NOW()),
(11, 6, 'Lavender', 4, NOW()),

-- materials for attribute_id = 7 (material)
(12, 7, 'Wood', 1, NOW()),
(13, 7, 'Herbal', 2, NOW()),
(14, 7, 'Synthetic', 3, NOW()),

-- usage_types for attribute_id = 8 (usage)
(15, 8, 'Temple', 1, NOW()),
(16, 8, 'Home', 2, NOW()),
(17, 8, 'Meditation', 3, NOW()),

-- certifications for attribute_id = 11 (certification)
(18, 11, 'ISO Certified', 1, NOW()),
(19, 11, 'Organic Certified', 2, NOW()),
(20, 11, 'Vegan Certified', 3, NOW()),

-- packaging_types for attribute_id = 12 (packaging_type)
(21, 12, 'Box', 1, NOW()),
(22, 12, 'Plastic Wrap', 2, NOW()),
(23, 12, 'Paper Bag', 3, NOW()),

-- countries for attribute_id = 13 (country_of_origin)
(24, 13, 'India', 1, NOW()),
(25, 13, 'Nepal', 2, NOW()),
(26, 13, 'Sri Lanka', 3, NOW()),

-- stock_status for attribute_id = 19 (stock_status)
(27, 19, 'In Stock', 1, NOW()),
(28, 19, 'Out of Stock', 2, NOW()),
(29, 19, 'Limited Stock', 3, NOW()),

-- fragrance_types for attribute_id = 24 (fragrance_type)
(30, 24, 'Floral', 1, NOW()),
(31, 24, 'Woody', 2, NOW()),
(32, 24, 'Spicy', 3, NOW()),
(33, 24, 'Citrus', 4, NOW()),

-- bead_materials for attribute_id = 25 (bead_material)
(34, 25, 'Wood', 1, NOW()),
(35, 25, 'Bone', 2, NOW()),
(36, 25, 'Glass', 3, NOW()),

-- material_finishes for attribute_id = 30 (material_finish)
(37, 30, 'Polished', 1, NOW()),
(38, 30, 'Matte', 2, NOW()),
(39, 30, 'Rough', 3, NOW()),

-- grain_sizes for attribute_id = 32 (powder_grain_size)
(40, 32, 'Fine', 1, NOW()),
(41, 32, 'Medium', 2, NOW()),
(42, 32, 'Coarse', 3, NOW()),

-- traditional_certs for attribute_id = 35 (traditional_certification)
(43, 35, 'Ayurvedic', 1, NOW()),
(44, 35, 'Traditional Handmade', 2, NOW()),

-- fabric_types for attribute_id = 36 (fabric_type)
(45, 36, 'Cotton', 1, NOW()),
(46, 36, 'Silk', 2, NOW()),
(47, 36, 'Linen', 3, NOW()),

-- metal_types for attribute_id = 37 (metal_type)
(48, 37, 'Brass', 1, NOW()),
(49, 37, 'Copper', 2, NOW()),
(50, 37, 'Bronze', 3, NOW()),

-- flower_types for attribute_id = 39 (flower_type)
(51, 39, 'Marigold', 1, NOW()),
(52, 39, 'Jasmine', 2, NOW()),
(53, 39, 'Rose', 3, NOW());


-- Temple

INSERT INTO catalog_attribute (
  id,
  catalog_attribute_key,
  category_id,
  sub_category_id,
  attribute_datatype_id,
  label,
  attribute_group_id,
  is_custom,
  is_required,
  is_filterable,
  is_summary,
  sort_order,
  attribute_icon_id,
  created_at,
  updated_at,
  allowed_values_source
) VALUES

-- 101: Aarti
(201, 'aarti_type', 2, 101, 5, 'Type of Aarti', 1, 0, 1, 1, 0, 1, NULL, NOW(), NOW(), 'aarti_types'),
(202, 'participate', 2, 101, 2, 'Participate in Aarti', 1, 0, 1, 1, 0, 2, NULL, NOW(), NOW(), NULL),
(203, 'time_slot', 2, 101, 5, 'Available Time Slots', 1, 0, 1, 1, 0, 3, NULL, NOW(), NOW(), 'aarti_time_slots'),
(204, 'duration_minutes', 2, 101, 3, 'Duration (minutes)', 1, 0, 0, 0, 0, 4, NULL, NOW(), NOW(), NULL),
(205, 'special_occasion', 2, 101, 2, 'Special Occasion Aarti', 1, 0, 0, 1, 0, 5, NULL, NOW(), NOW(), NULL),
(206, 'prasad_included', 2, 101, 2, 'Prasad Included', 1, 0, 0, 1, 0, 6, NULL, NOW(), NOW(), NULL),

-- 102: Pooja
(207, 'pooja_type', 2, 102, 5, 'Type of Pooja', 1, 0, 1, 1, 0, 7, NULL, NOW(), NOW(), 'pooja_types'),
(208, 'booking_date', 2, 102, 4, 'Date of Pooja Booking', 1, 0, 1, 1, 0, 8, NULL, NOW(), NOW(), NULL),
(209, 'number_of_people', 2, 102, 3, 'Number of Participants', 1, 0, 0, 0, 0, 9, NULL, NOW(), NOW(), NULL),
(210, 'special_requests', 2, 102, 1, 'Special Requests', 1, 1, 0, 0, 0, 10, NULL, NOW(), NOW(), NULL),

-- 103: Prasad
(211, 'delivery_available', 2, 103, 2, 'Prasad Delivery Available', 1, 0, 1, 1, 0, 11, NULL, NOW(), NOW(), NULL),
(212, 'delivery_location', 2, 103, 1, 'Delivery Location', 1, 0, 0, 0, 0, 12, NULL, NOW(), NOW(), NULL),
(213, 'prasad_type', 2, 103, 5, 'Type of Prasad', 1, 0, 1, 1, 0, 13, NULL, NOW(), NOW(), 'prasad_types'),

-- 104: Donation
(214, 'donation_amount', 2, 104, 3, 'Amount to Donate', 1, 0, 1, 0, 0, 14, NULL, NOW(), NOW(), NULL),
(215, 'donation_purpose', 2, 104, 5, 'Purpose of Donation', 1, 0, 1, 1, 0, 15, NULL, NOW(), NOW(), 'donation_purposes'),
(216, 'anonymous_donation', 2, 104, 2, 'Anonymous Donation', 1, 0, 0, 0, 0, 16, NULL, NOW(), NOW(), NULL),

-- 105: VIP Pass
(217, 'vip_pass_available', 2, 105, 2, 'VIP Pass Available', 1, 0, 1, 1, 0, 17, NULL, NOW(), NOW(), NULL),
(218, 'priority_level', 2, 105, 5, 'VIP Priority Level', 1, 0, 1, 1, 0, 18, NULL, NOW(), NOW(), 'vip_priority_levels'),
(219, 'validity_period', 2, 105, 4, 'Validity Period', 1, 0, 0, 0, 0, 19, NULL, NOW(), NOW(), NULL),

-- 106: Guide Tour
(220, 'guide_language', 2, 106, 5, 'Language of Guide', 1, 0, 1, 0, 0, 20, NULL, NOW(), NOW(), 'guide_languages'),
(221, 'tour_duration', 2, 106, 3, 'Tour Duration (minutes)', 1, 0, 0, 0, 0, 21, NULL, NOW(), NOW(), NULL),
(222, 'group_size_limit', 2, 106, 3, 'Group Size Limit', 1, 0, 0, 0, 0, 22, NULL, NOW(), NOW(), NULL),

-- 107: All Temple Combo
(223, 'combo_options', 2, 107, 5, 'Available Combo Options', 1, 0, 1, 0, 0, 23, NULL, NOW(), NOW(), 'combo_options'),
(224, 'combo_price', 2, 107, 3, 'Combo Price', 1, 0, 0, 0, 0, 24, NULL, NOW(), NOW(), NULL),

-- 108: Festival Events
(225, 'festival_type', 2, 108, 5, 'Type of Festival', 1, 0, 1, 1, 0, 25, NULL, NOW(), NOW(), 'festival_types'),
(226, 'festival_date', 2, 108, 4, 'Festival Date', 1, 0, 1, 1, 0, 26, NULL, NOW(), NOW(), NULL),
(227, 'special_performances', 2, 108, 2, 'Special Performances', 1, 0, 0, 0, 0, 27, NULL, NOW(), NOW(), NULL),

-- 109: Temple Shop
(228, 'item_category', 2, 109, 5, 'Category of Item', 1, 0, 1, 0, 0, 28, NULL, NOW(), NOW(), 'shop_item_categories'),
(229, 'price_range', 2, 109, 1, 'Price Range', 1, 0, 1, 0, 0, 29, NULL, NOW(), NOW(), NULL),
(230, 'availability', 2, 109, 2, 'Availability', 1, 0, 1, 0, 0, 30, NULL, NOW(), NOW(), NULL),

-- 110: Live Streaming
(231, 'streaming_quality', 2, 110, 5, 'Streaming Quality', 1, 0, 0, 0, 0, 31, NULL, NOW(), NOW(), 'streaming_qualities'),
(232, 'available_languages', 2, 110, 5, 'Languages Available', 1, 0, 1, 0, 0, 32, NULL, NOW(), NOW(), 'streaming_languages'),
(233, 'streaming_date', 2, 110, 4, 'Streaming Date and Time', 1, 0, 1, 1, 0, 33, NULL, NOW(), NOW(), NULL),
(234, 'access_type', 2, 110, 5, 'Access Type', 1, 0, 1, 1, 0, 34, NULL, NOW(), NOW(), 'streaming_access_types');


INSERT INTO attribute_allowed_value (id, attribute_id, value, sort_order, created_at) VALUES
-- aarti_type (attribute_id=201)
(1001, 201, 'morning', 1, NOW()),
(1002, 201, 'evening', 2, NOW()),
(1003, 201, 'special', 3, NOW()),

-- time_slot (attribute_id=203)
(1004, 203, '6am-7am', 1, NOW()),
(1005, 203, '7am-8am', 2, NOW()),
(1006, 203, '6pm-7pm', 3, NOW()),
(1007, 203, '7pm-8pm', 4, NOW()),

-- pooja_type (attribute_id=207)
(1008, 207, 'ganesh_pooja', 1, NOW()),
(1009, 207, 'lakshmi_pooja', 2, NOW()),
(1010, 207, 'saraswati_pooja', 3, NOW()),

-- prasad_type (attribute_id=213)
(1011, 213, 'ladoos', 1, NOW()),
(1012, 213, 'fruits', 2, NOW()),
(1013, 213, 'sweet_dish', 3, NOW()),

-- donation_purpose (attribute_id=215)
(1014, 215, 'temple_maintenance', 1, NOW()),
(1015, 215, 'charity', 2, NOW()),
(1016, 215, 'festival_fund', 3, NOW()),

-- priority_level (attribute_id=218)
(1017, 218, 'gold', 1, NOW()),
(1018, 218, 'platinum', 2, NOW()),
(1019, 218, 'diamond', 3, NOW()),

-- guide_language (attribute_id=220)
(1020, 220, 'english', 1, NOW()),
(1021, 220, 'hindi', 2, NOW()),
(1022, 220, 'regional', 3, NOW()),

-- combo_options (attribute_id=223)
(1023, 223, 'full_temple_experience', 1, NOW()),
(1024, 223, 'festival_package', 2, NOW()),
(1025, 223, 'custom_combo', 3, NOW()),

-- festival_type (attribute_id=225)
(1026, 225, 'diwali', 1, NOW()),
(1027, 225, 'navratri', 2, NOW()),
(1028, 225, 'holi', 3, NOW()),

-- item_category (attribute_id=228)
(1029, 228, 'idols', 1, NOW()),
(1030, 228, 'prasad', 2, NOW()),
(1031, 228, 'books', 3, NOW()),
(1032, 228, 'souvenirs', 4, NOW()),

-- streaming_quality (attribute_id=231)
(1033, 231, 'sd', 1, NOW()),
(1034, 231, 'hd', 2, NOW()),
(1035, 231, 'full_hd', 3, NOW()),

-- available_languages (attribute_id=232)
(1036, 232, 'english', 1, NOW()),
(1037, 232, 'hindi', 2, NOW()),
(1038, 232, 'regional', 3, NOW()),

-- access_type (attribute_id=234)
(1039, 234, 'free', 1, NOW()),
(1040, 234, 'paid', 2, NOW()),
(1041, 234, 'subscription', 3, NOW());


-- Priest

INSERT INTO catalog_attribute (
  id,
  catalog_attribute_key,
  category_id,
  sub_category_id,
  attribute_datatype_id,
  label,
  attribute_group_id,
  is_custom,
  is_required,
  is_filterable,
  is_summary,
  sort_order,
  attribute_icon_id,
  created_at,
  updated_at,
  allowed_values_source
) VALUES

-- Temple Priest
(301, 'years_experience', 3, 201, 3, 'Years of Experience', 1, 0, 1, 1, 0, 1, NULL, NOW(), NOW(), NULL),
(302, 'languages_spoken', 3, 201, 5, 'Languages Spoken', 1, 0, 1, 1, 0, 2, NULL, NOW(), NOW(), 'priest_languages'),
(303, 'available_time_slots', 3, 201, 5, 'Available Time Slots', 1, 0, 1, 1, 0, 3, NULL, NOW(), NOW(), 'priest_time_slots'),

-- Home Priest
(304, 'service_area', 3, 202, 1, 'Service Area (Location)', 1, 0, 1, 0, 0, 4, NULL, NOW(), NOW(), NULL),
(305, 'travel_fee_applicable', 3, 202, 2, 'Is Travel Fee Applicable?', 1, 0, 0, 0, 0, 5, NULL, NOW(), NOW(), NULL),
(306, 'available_time_slots', 3, 202, 5, 'Available Time Slots', 1, 0, 1, 1, 0, 6, NULL, NOW(), NOW(), 'priest_time_slots'),

-- Special Event
(307, 'specialized_events', 3, 203, 5, 'Specialized Event Types', 1, 0, 1, 1, 0, 7, NULL, NOW(), NOW(), 'event_types'),
(308, 'advance_booking_days', 3, 203, 3, 'Advance Booking Notice (Days)', 1, 0, 1, 0, 0, 8, NULL, NOW(), NOW(), NULL),
(309, 'fee_details', 3, 203, 1, 'Fee Structure Details', 1, 1, 0, 0, 0, 9, NULL, NOW(), NOW(), NULL),

-- Pooja Specialist
(310, 'deity_specialization', 3, 204, 5, 'Deities Specialized In', 1, 0, 1, 1, 0, 10, NULL, NOW(), NOW(), 'deity_list'),
(311, 'pooja_types', 3, 204, 5, 'Types of Poojas Offered', 1, 0, 1, 1, 0, 11, NULL, NOW(), NOW(), 'pooja_types'),
(312, 'years_specialized', 3, 204, 3, 'Years of Specialization', 1, 0, 0, 0, 0, 12, NULL, NOW(), NOW(), NULL),

-- Spiritual Guide
(313, 'guidance_topics', 3, 205, 5, 'Topics Covered', 1, 0, 1, 1, 0, 13, NULL, NOW(), NOW(), 'guidance_topics'),
(314, 'session_types', 3, 205, 5, 'Session Types Available', 1, 0, 1, 1, 0, 14, NULL, NOW(), NOW(), 'session_types'),
(315, 'session_duration', 3, 205, 3, 'Session Duration (minutes)', 1, 0, 0, 0, 0, 15, NULL, NOW(), NOW(), NULL),

-- Regional Specialist
(316, 'supported_languages_regions', 3, 206, 5, 'Supported Languages/Regions', 1, 0, 1, 1, 0, 16, NULL, NOW(), NOW(), 'priest_languages'),
(317, 'regional_rituals_expertise', 3, 206, 2, 'Expertise in Regional Rituals', 1, 0, 0, 0, 0, 17, NULL, NOW(), NOW(), NULL),

-- Quick Book Priest
(318, 'immediate_availability', 3, 207, 2, 'Available for Immediate Booking', 1, 0, 1, 1, 0, 18, NULL, NOW(), NOW(), NULL),
(319, 'avg_response_time', 3, 207, 3, 'Average Response Time (minutes)', 1, 0, 0, 0, 0, 19, NULL, NOW(), NOW(), NULL),

-- Online Rituals
(320, 'video_platforms_supported', 3, 208, 5, 'Supported Video Call Platforms', 1, 0, 1, 0, 0, 20, NULL, NOW(), NOW(), 'video_platforms'),
(321, 'session_durations', 3, 208, 5, 'Session Durations', 1, 0, 1, 0, 0, 21, NULL, NOW(), NOW(), 'session_durations'),
(322, 'supported_time_zones', 3, 208, 1, 'Supported Time Zones', 1, 0, 1, 0, 0, 22, NULL, NOW(), NOW(), NULL),

-- Festival Priest
(323, 'festivals_specialized', 3, 209, 5, 'Festivals Specialized In', 1, 0, 1, 1, 0, 23, NULL, NOW(), NOW(), 'festival_types'),
(324, 'available_festival_dates', 3, 209, 5, 'Available Dates for Festivals', 1, 0, 1, 1, 0, 24, NULL, NOW(), NOW(), NULL),

-- Tantrik Priest
(325, 'rituals_offered', 3, 210, 5, 'Tantrik Rituals Offered', 1, 0, 1, 1, 0, 25, NULL, NOW(), NOW(), 'tantrik_rituals'),
(326, 'confidentiality_assured', 3, 210, 2, 'Confidentiality Assured', 1, 0, 0, 0, 0, 26, NULL, NOW(), NOW(), NULL);


INSERT INTO attribute_allowed_value (
  id,
  attribute_id,
  value,
  sort_order,
  created_at
) VALUES

-- priest_languages (attribute_id: assume 302)
(2001, 302, 'Hindi', 1, NOW()),
(2002, 302, 'English', 2, NOW()),
(2003, 302, 'Tamil', 3, NOW()),
(2004, 302, 'Telugu', 4, NOW()),
(2005, 302, 'Kannada', 5, NOW()),
(2006, 302, 'Marathi', 6, NOW()),

-- priest_time_slots (attribute_id: assume 303)
(2007, 303, '06:00 AM - 07:00 AM', 1, NOW()),
(2008, 303, '07:00 AM - 08:00 AM', 2, NOW()),
(2009, 303, '05:00 PM - 06:00 PM', 3, NOW()),
(2010, 303, '06:00 PM - 07:00 PM', 4, NOW()),
(2011, 303, '07:00 PM - 08:00 PM', 5, NOW()),

-- event_types (attribute_id: assume 304)
(2012, 304, 'Wedding', 1, NOW()),
(2013, 304, 'Naming Ceremony', 2, NOW()),
(2014, 304, 'Housewarming', 3, NOW()),
(2015, 304, 'Thread Ceremony', 4, NOW()),
(2016, 304, 'Funeral', 5, NOW()),

-- deity_list (attribute_id: assume 305)
(2017, 305, 'Lord Ganesha', 1, NOW()),
(2018, 305, 'Goddess Lakshmi', 2, NOW()),
(2019, 305, 'Lord Shiva', 3, NOW()),
(2020, 305, 'Goddess Saraswati', 4, NOW()),
(2021, 305, 'Lord Vishnu', 5, NOW()),

-- pooja_types (attribute_id: assume 306)
(2022, 306, 'Ganesh Pooja', 1, NOW()),
(2023, 306, 'Lakshmi Pooja', 2, NOW()),
(2024, 306, 'Navagraha Pooja', 3, NOW()),
(2025, 306, 'Satyanarayan Pooja', 4, NOW()),
(2026, 306, 'Durga Pooja', 5, NOW()),

-- guidance_topics (attribute_id: assume 307)
(2027, 307, 'Meditation', 1, NOW()),
(2028, 307, 'Yoga', 2, NOW()),
(2029, 307, 'Vedic Astrology', 3, NOW()),
(2030, 307, 'Spiritual Discourses', 4, NOW()),
(2031, 307, 'Life Coaching', 5, NOW()),

-- session_types (attribute_id: assume 308)
(2032, 308, 'Individual', 1, NOW()),
(2033, 308, 'Group', 2, NOW()),
(2034, 308, 'Family', 3, NOW()),
(2035, 308, 'Corporate', 4, NOW()),

-- video_platforms (attribute_id: assume 309)
(2036, 309, 'YouTube', 1, NOW()),
(2037, 309, 'Zoom', 2, NOW()),
(2038, 309, 'Google Meet', 3, NOW()),
(2039, 309, 'Microsoft Teams', 4, NOW()),

-- session_durations (attribute_id: assume 310)
(2040, 310, '15 minutes', 1, NOW()),
(2041, 310, '30 minutes', 2, NOW()),
(2042, 310, '45 minutes', 3, NOW()),
(2043, 310, '60 minutes', 4, NOW()),

-- festival_types (attribute_id: assume 311)
(2044, 311, 'Diwali', 1, NOW()),
(2045, 311, 'Holi', 2, NOW()),
(2046, 311, 'Navratri', 3, NOW()),
(2047, 311, 'Makar Sankranti', 4, NOW()),

-- tantrik_rituals (attribute_id: assume 312)
(2048, 312, 'Mahavidya Pooja', 1, NOW()),
(2049, 312, 'Navagraha Shanti', 2, NOW()),
(2050, 312, 'Chandi Homa', 3, NOW()),
(2051, 312, 'Shakti Pooja', 4, NOW()),
(2052, 312, 'Bhoot Shanti', 5, NOW());


-- Astrologer

INSERT INTO catalog_attribute (
  id,
  catalog_attribute_key,
  category_id,
  sub_category_id,
  attribute_datatype_id,
  label,
  attribute_group_id,
  is_custom,
  is_required,
  is_filterable,
  is_summary,
  sort_order,
  attribute_icon_id,
  created_at,
  updated_at,
  allowed_values_source
) VALUES

-- 401: Astrology Consultation
(401, 'consultation_type', 4, 301, 5, 'Consultation Type', 1, 0, 1, 1, 1, 1, NULL, NOW(), NOW(), 'consultation_types'),
(402, 'appointment_date', 4, 301, 4, 'Appointment Date & Time', 1, 0, 1, 1, 1, 2, NULL, NOW(), NOW(), NULL),
(403, 'session_duration', 4, 301, 3, 'Session Duration (minutes)', 1, 0, 0, 0, 0, 3, NULL, NOW(), NOW(), 'session_durations'),
(404, 'consultation_language', 4, 301, 5, 'Language of Consultation', 1, 0, 1, 0, 0, 4, NULL, NOW(), NOW(), 'priest_languages'),

-- 402: Birth Chart (Janam Kundali)
(405, 'birth_date', 4, 302, 4, 'Date of Birth', 1, 0, 1, 1, 1, 5, NULL, NOW(), NOW(), NULL),
(406, 'birth_time', 4, 302, 5, 'Time of Birth', 1, 0, 1, 1, 1, 6, NULL, NOW(), NOW(), 'time_slots'),
(407, 'birth_place', 4, 302, 1, 'Place of Birth', 1, 0, 1, 1, 1, 7, NULL, NOW(), NOW(), NULL),

-- 403: Gemstone Recommendation
(408, 'gemstone', 4, 303, 5, 'Recommended Gemstone', 1, 0, 1, 1, 1, 8, NULL, NOW(), NOW(), 'gemstone_types'),
(409, 'metal', 4, 303, 5, 'Metal Type', 1, 0, 0, 0, 0, 9, NULL, NOW(), NOW(), 'metal_types'),
(410, 'price_range', 4, 303, 3, 'Price Range', 1, 0, 1, 0, 0, 10, NULL, NOW(), NOW(), NULL),

-- 404: Yantra
(411, 'yantra_type', 4, 304, 5, 'Yantra Type', 1, 0, 1, 1, 1, 11, NULL, NOW(), NOW(), 'yantra_types'),
(412, 'material', 4, 304, 5, 'Material', 1, 0, 0, 0, 0, 12, NULL, NOW(), NOW(), 'material_types'),

-- 405: Palmistry Reading
(413, 'focus_area', 4, 305, 5, 'Focus Area', 1, 0, 1, 1, 1, 13, NULL, NOW(), NOW(), 'palmistry_areas'),
(414, 'reading_duration', 4, 305, 3, 'Reading Duration (minutes)', 1, 0, 0, 0, 0, 14, NULL, NOW(), NOW(), 'session_durations'),

-- 406: Numerology Report
(415, 'report_type', 4, 306, 5, 'Report Type', 1, 0, 1, 1, 1, 15, NULL, NOW(), NOW(), 'numerology_report_types'),
(416, 'name_for_analysis', 4, 306, 1, 'Name for Analysis', 1, 0, 1, 0, 0, 16, NULL, NOW(), NOW(), NULL),

-- 407: Astrology Books
(417, 'book_category', 4, 307, 5, 'Book Category', 1, 0, 1, 1, 1, 17, NULL, NOW(), NOW(), 'book_categories'),
(418, 'author', 4, 307, 1, 'Author', 1, 0, 0, 0, 0, 18, NULL, NOW(), NOW(), NULL),

-- 408: Astrology Software Access
(419, 'software_name', 4, 308, 1, 'Software Name', 1, 0, 1, 1, 1, 19, NULL, NOW(), NOW(), NULL),
(420, 'subscription_period', 4, 308, 4, 'Subscription Period', 1, 0, 1, 0, 0, 20, NULL, NOW(), NOW(), NULL),

-- 409: Kundli Matching (Love/Marriage Matching)
(421, 'partner_birth_details', 4, 309, 1, 'Partner Birth Details', 1, 0, 1, 1, 1, 21, NULL, NOW(), NOW(), NULL),
(422, 'matching_type', 4, 309, 5, 'Matching Type', 1, 0, 1, 0, 0, 22, NULL, NOW(), NOW(), 'matching_types'),

-- 410: Muhurta Selection
(423, 'event_type', 4, 310, 5, 'Event Type', 1, 0, 1, 1, 1, 23, NULL, NOW(), NOW(), 'event_types'),
(424, 'preferred_date_range', 4, 310, 4, 'Preferred Date Range', 1, 0, 0, 0, 0, 24, NULL, NOW(), NOW(), NULL),

-- 411: Astrological Remedies
(425, 'remedy_type', 4, 311, 5, 'Remedy Type', 1, 0, 1, 1, 1, 25, NULL, NOW(), NOW(), 'remedy_types'),
(426, 'remedy_duration_days', 4, 311, 3, 'Recommended Duration (days)', 1, 0, 0, 0, 0, 26, NULL, NOW(), NOW(), NULL);


INSERT INTO attribute_allowed_value (
  id,
  attribute_id,
  value,
  sort_order,
  created_at
) VALUES

-- consultation_types (attribute_id = 401)
(3001, 401, 'Personal Horoscope Reading', 1, NOW()),
(3002, 401, 'Career Guidance', 2, NOW()),
(3003, 401, 'Financial Forecast', 3, NOW()),
(3004, 401, 'Health Analysis', 4, NOW()),
(3005, 401, 'Marriage & Relationships', 5, NOW()),

-- gemstone_types (attribute_id = 408)
(3010, 408, 'Blue Sapphire', 1, NOW()),
(3011, 408, 'Red Coral', 2, NOW()),
(3012, 408, 'Yellow Sapphire', 3, NOW()),
(3013, 408, 'Emerald', 4, NOW()),
(3014, 408, 'Pearl', 5, NOW()),
(3015, 408, 'Diamond', 6, NOW()),

-- metal_types (attribute_id = 409)
(3020, 409, 'Gold', 1, NOW()),
(3021, 409, 'Silver', 2, NOW()),
(3022, 409, 'Bronze', 3, NOW()),
(3023, 409, 'Copper', 4, NOW()),

-- yantra_types (attribute_id = 411)
(3030, 411, 'Shree Yantra', 1, NOW()),
(3031, 411, 'Mahalaxmi Yantra', 2, NOW()),
(3032, 411, 'Navagraha Yantra', 3, NOW()),
(3033, 411, 'Saraswati Yantra', 4, NOW()),
(3034, 411, 'Kubera Yantra', 5, NOW()),

-- material_types (attribute_id = 412)
(3040, 412, 'Copper', 1, NOW()),
(3041, 412, 'Brass', 2, NOW()),
(3042, 412, 'Silver', 3, NOW()),
(3043, 412, 'Gold Plated', 4, NOW()),

-- palmistry_areas (attribute_id = 413)
(3050, 413, 'Career & Business', 1, NOW()),
(3051, 413, 'Love & Relationships', 2, NOW()),
(3052, 413, 'Health & Wellness', 3, NOW()),
(3053, 413, 'Financial Stability', 4, NOW()),

-- session_durations (attribute_id = 403, 414)
(3060, 403, '15 minutes', 1, NOW()),
(3061, 403, '30 minutes', 2, NOW()),
(3062, 403, '45 minutes', 3, NOW()),
(3063, 403, '60 minutes', 4, NOW()),

(3064, 414, '15 minutes', 1, NOW()),
(3065, 414, '30 minutes', 2, NOW()),
(3066, 414, '45 minutes', 3, NOW()),
(3067, 414, '60 minutes', 4, NOW()),

-- numerology_report_types (attribute_id = 415)
(3070, 415, 'Life Path Report', 1, NOW()),
(3071, 415, 'Name Analysis', 2, NOW()),
(3072, 415, 'Compatibility Report', 3, NOW()),
(3073, 415, 'Yearly Forecast', 4, NOW()),

-- book_categories (attribute_id = 417)
(3080, 417, 'Astrology Basics', 1, NOW()),
(3081, 417, 'Vedic Astrology', 2, NOW()),
(3082, 417, 'Numerology', 3, NOW()),
(3083, 417, 'Palmistry', 4, NOW()),
(3084, 417, 'Spirituality & Rituals', 5, NOW()),

-- matching_types (attribute_id = 422)
(3090, 422, 'Love Matching', 1, NOW()),
(3091, 422, 'Kundli Matching', 2, NOW()),
(3092, 422, 'Career Compatibility', 3, NOW()),
(3093, 422, 'Financial Compatibility', 4, NOW()),

-- event_types (attribute_id = 423)
(3100, 423, 'Wedding', 1, NOW()),
(3101, 423, 'Housewarming', 2, NOW()),
(3102, 423, 'Business Opening', 3, NOW()),
(3103, 423, 'Naming Ceremony', 4, NOW()),
(3104, 423, 'Religious Festival', 5, NOW()),

-- remedy_types (attribute_id = 425)
(3110, 425, 'Yantra Installation', 1, NOW()),
(3111, 425, 'Gemstone Wearing', 2, NOW()),
(3112, 425, 'Mantra Chanting', 3, NOW()),
(3113, 425, 'Ritual Pooja', 4, NOW()),
(3114, 425, 'Fasting/Observance', 5, NOW());


-- Kathavachak


INSERT INTO catalog_attribute
(id, catalog_attribute_key, category_id, sub_category_id, attribute_datatype_id, label, attribute_group_id, is_custom, is_required, is_filterable, is_summary, sort_order, attribute_icon_id, created_at, updated_at, allowed_values_source)
VALUES
(4001, 'ramayana_chapter_1', 5, 401, 1, 'Ramayana Chapter 1', 401, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4002, 'ramayana_chapter_2', 5, 401, 1, 'Ramayana Chapter 2', 401, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4003, 'ramayana_chapter_3', 5, 401, 1, 'Ramayana Chapter 3', 401, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),

(4004, 'bhagavad_gita_chapter_1', 5, 402, 1, 'Bhagavad Gita Chapter 1', 402, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4005, 'bhagavad_gita_chapter_2', 5, 402, 1, 'Bhagavad Gita Chapter 2', 402, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4006, 'bhagavad_gita_chapter_3', 5, 402, 1, 'Bhagavad Gita Chapter 3', 402, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),

(4007, 'mahabharata_episode_1', 5, 403, 1, 'Mahabharata Episode 1', 403, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4008, 'mahabharata_episode_2', 5, 403, 1, 'Mahabharata Episode 2', 403, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4009, 'mahabharata_episode_3', 5, 403, 1, 'Mahabharata Episode 3', 403, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),

(4010, 'spiritual_talk_1', 5, 404, 1, 'Spiritual Talk 1', 404, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4011, 'spiritual_talk_2', 5, 404, 1, 'Spiritual Talk 2', 404, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4012, 'spiritual_talk_3', 5, 404, 1, 'Spiritual Talk 3', 404, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),

(4013, 'online_storytelling_1', 5, 405, 1, 'Online Storytelling 1', 405, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4014, 'online_storytelling_2', 5, 405, 1, 'Online Storytelling 2', 405, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4015, 'online_storytelling_3', 5, 405, 1, 'Online Storytelling 3', 405, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),

(4016, 'live_storytelling_1', 5, 406, 1, 'Live Storytelling 1', 406, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4017, 'live_storytelling_2', 5, 406, 1, 'Live Storytelling 2', 406, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4018, 'live_storytelling_3', 5, 406, 1, 'Live Storytelling 3', 406, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),

(4019, 'bedtime_stories', 5, 407, 1, 'Bedtime Stories', 407, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4020, 'moral_stories', 5, 407, 1, 'Moral Stories', 407, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4021, 'interactive_storytelling', 5, 407, 1, 'Interactive Storytelling', 407, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),

(4022, 'diwali_katha', 5, 408, 1, 'Diwali Katha', 408, 0, 0, 1, 0, 1, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4023, 'navratri_katha', 5, 408, 1, 'Navratri Katha', 408, 0, 0, 1, 0, 2, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL),
(4024, 'janmashtami_katha', 5, 408, 1, 'Janmashtami Katha', 408, 0, 0, 1, 0, 3, NULL, '2025-09-17 18:17:05', '2025-09-17 18:17:05', NULL);


INSERT INTO attribute_allowed_value
(id, attribute_id, value, sort_order, created_at)
VALUES
(4001, 501, 'Ramayana Chapter 1', 1, NOW()),
(4002, 502, 'Ramayana Chapter 2', 1, NOW()),
(4003, 503, 'Ramayana Chapter 3', 1, NOW()),
(4004, 504, 'Bhagavad Gita Chapter 1', 1, NOW()),
(4005, 505, 'Bhagavad Gita Chapter 2', 1, NOW()),
(4006, 506, 'Bhagavad Gita Chapter 3', 1, NOW()),
(4007, 507, 'Mahabharata Episode 1', 1, NOW()),
(4008, 508, 'Mahabharata Episode 2', 1, NOW()),
(4009, 509, 'Mahabharata Episode 3', 1, NOW()),
(4010, 510, 'Spiritual Talk 1', 1, NOW()),
(4011, 511, 'Spiritual Talk 2', 1, NOW()),
(4012, 512, 'Spiritual Talk 3', 1, NOW()),
(4013, 513, 'Online Storytelling 1', 1, NOW()),
(4014, 514, 'Online Storytelling 2', 1, NOW()),
(4015, 515, 'Online Storytelling 3', 1, NOW()),
(4016, 516, 'Live Storytelling 1', 1, NOW()),
(4017, 517, 'Live Storytelling 2', 1, NOW()),
(4018, 518, 'Live Storytelling 3', 1, NOW()),
(4019, 519, 'Bedtime Stories', 1, NOW()),
(4020, 520, 'Moral Stories', 1, NOW()),
(4021, 521, 'Interactive Storytelling', 1, NOW()),
(4022, 522, 'Diwali Katha', 1, NOW()),
(4023, 523, 'Navratri Katha', 1, NOW()),
(4024, 524, 'Janmashtami Katha', 1, NOW());


------------------------------------------------Temporary Development-------------------------------------------------------------------




INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(1, 1, 'Phone', 'Fragrant incense sticks used in temples and homes.', 1, 'Product', NULL, NOW(), NOW()),
(2, 1, 'Prayer Beads (Mala)', 'Beads used for meditation and chanting mantras.', 1, 'Product', NULL, NOW(), NOW()),
(3, 1, 'Holy Water Bottle', 'Bottled holy water from sacred temples.', 1, 'Product', NULL, NOW(), NOW()),
(4, 1, 'Camphor Tablets', 'Used in aarti and pooja rituals for lighting and fragrance.', 1, 'Product', NULL, NOW(), NOW()),
(5, 1, 'Dhoop Sticks', 'Special incense sticks for pooja with a rich fragrance.', 1, 'Product', NULL, NOW(), NOW()),
(6, 1, 'Pooja Thali Set', 'Complete pooja thali set including diya, bell, and more.', 1, 'Product', NULL, NOW(), NOW());

-- Category 1: Temple (IDs 101 - 200)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(101, 2, 'Aarti', 'Participate in temple aartis', 1, 'Temple Service', NULL, NOW(), NOW()),
(102, 2, 'Pooja', 'Book a ritual pooja', 1, 'Temple Service', NULL, NOW(), NOW()),
(103, 2, 'Prasad', 'Get prasad delivered', 1, 'Temple Service', NULL, NOW(), NOW()),
(104, 2, 'Donation', 'Donate to temple causes', 1, 'Temple Service', NULL, NOW(), NOW()),
(105, 2, 'VIP Pass', 'Priority entry options', 1, 'Temple Service', NULL, NOW(), NOW()),
(106, 2, 'Guide Tour', 'Guided temple experiences', 1, 'Temple Service', NULL, NOW(), NOW()),
(107, 2, 'All Temple Combo', 'Make your own combo booking', 1, 'Temple Service', NULL, NOW(), NOW()),
(108, 2, 'Festival Events', 'Participate in special temple festivals and seasonal celebrations', 1, 'Temple Service', NULL, NOW(), NOW()),
(109, 2, 'Temple Shop', 'Purchase religious items and souvenirs', 1, 'Temple Service', NULL, NOW(), NOW()),
(110, 2, 'Live Streaming', 'Watch live temple rituals and darshan online', 1, 'Temple Service', NULL, NOW(), NOW());


-- Category 2: Priest (IDs 201 - 300)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(201, 3, 'Temple Priest', 'Priests assigned to specific temples', 1, 'Priest Type', NULL, NOW(), NOW()),
(202, 3, 'Home Priest', 'Perform rituals at user’s home', 1, 'Priest Type', NULL, NOW(), NOW()),
(203, 3, 'Special Event', 'Specialized in weddings, naming, etc.', 1, 'Priest Type', NULL, NOW(), NOW()),
(204, 3, 'Pooja Specialist', 'Expert in particular deities/rituals', 1, 'Priest Type', NULL, NOW(), NOW()),
(205, 3, 'Spiritual Guide', 'Offers discourses, meditation, and guidance', 1, 'Priest Type', NULL, NOW(), NOW()),
(206, 3, 'Regional Specialist', 'Perform rituals in specific languages/regions', 1, 'Priest Type', NULL, NOW(), NOW()),
(207, 3, 'Quick Book Priest', 'Priests available on-demand or urgently', 1, 'Priest Type', NULL, NOW(), NOW()),
(208, 3, 'Online Rituals', 'Priests available via video call', 1, 'Priest Type', NULL, NOW(), NOW()),
(209, 3, 'Festival Priest', 'Focused on major festival rituals', 1, 'Priest Type', NULL, NOW(), NOW()),
(210, 3, 'Tantrik Priest', 'Priests specialized in Tantrik rituals and poojas', 1, 'Priest Type', NULL, NOW(), NOW());


-- Category 4: Astrologer (IDs 301 - 400)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(301, 4, 'Astrology Consultation', 'Personal horoscope reading and guidance from expert astrologers.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(302, 4, 'Birth Chart (Janam Kundali)', 'Detailed birth chart based on date, time, and place of birth.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(303, 4, 'Gemstone Recommendation', 'Astrologer recommended gemstones to balance planetary influences.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(304, 4, 'Yantra', 'Sacred geometric diagrams used as astrological remedies.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(305, 4, 'Palmistry Reading', 'Analysis of palm lines to predict future events.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(306, 4, 'Numerology Report', 'Personalized report based on numbers and dates.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(307, 4, 'Astrology Books', 'Books on astrology, planetary positions, and rituals.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(308, 4, 'Astrology Software Access', 'Access to advanced astrology calculation tools.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(309, 4, 'Horoscope Matching', 'Matchmaking services based on astrological compatibility.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(310, 4, 'Muhurta Selection', 'Choosing auspicious dates and times for important events.', 1, 'Astrology Service', NULL, NOW(), NOW()),
(311, 4, 'Astrological Remedies', 'Yantras, gemstones, mantras and rituals for problems.', 1, 'Astrology Service', NULL, NOW(), NOW());

-- Category 5: Kathavachak Service (IDs 401 - 500)
INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
(401, 5, 'Ramayana Kathavachak', 'Storytelling sessions of Ramayana', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(402, 5, 'Bhagavad Gita Kathavachak', 'Discourses and explanations of Bhagavad Gita', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(403, 5, 'Mahabharata Kathavachak', 'Narration of Mahabharata episodes', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(404, 5, 'Spiritual Discourses', 'General spiritual talks and teachings', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(405, 5, 'Online Kathavachak', 'Virtual storytelling sessions', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(406, 5, 'Onsite Katha Visit', 'Kathavachak visits your area for live storytelling events and discourses.', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(407, 5, 'Children Stories Kathavachak', 'Storytelling sessions specially for children', 1, 'Kathavachak Service', NULL, NOW(), NOW()),
(408, 5, 'Festival Special Kathavachak', 'Special katha sessions during festivals', 1, 'Kathavachak Service', NULL, NOW(), NOW());



INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, subcategory_type, parent_subcategory_id, created_at, updated_at
) VALUES
-- Incense Sticks (1)
(1001, 1, 'Iphone 14', 'Sandalwood-scented incense sticks for calming atmosphere.', 1, 'Product', 1, NOW(), NOW()),
(1002, 1, 'Iphone 15', 'Jasmine-scented sticks ideal for evening aarti.', 1, 'Product', 1, NOW(), NOW()),
(1003, 1, 'Iphone 16', 'Lavender incense for meditation and focus.', 1, 'Product', 1, NOW(), NOW()),

-- Prayer Beads (Mala) (2)
(1004, 1, 'Tulsi Mala', '108-bead Tulsi mala for chanting.', 1, 'Product', 2, NOW(), NOW()),
(1005, 1, 'Rudraksha Mala', 'Sacred Rudraksha beads for Lord Shiva devotees.', 1, 'Product', 2, NOW(), NOW()),
(1006, 1, 'Sphatik Mala', 'Crystal beads for positive energy.', 1, 'Product', 2, NOW(), NOW()),

-- Holy Water Bottle (3)
(1007, 1, 'Ganga Jal 250ml', 'Holy water from the Ganges.', 1, 'Product', 3, NOW(), NOW()),
(1008, 1, 'Yamuna Jal 250ml', 'Sacred Yamuna river water.', 1, 'Product', 3, NOW(), NOW()),
(1009, 1, 'Sangam Jal 500ml', 'Water from the confluence of rivers.', 1, 'Product', 3, NOW(), NOW()),

-- Camphor Tablets (4)
(1010, 1, 'Pure Bhimseni Camphor', 'High-quality camphor for aarti.', 1, 'Product', 4, NOW(), NOW()),
(1011, 1, 'Round Camphor Tablets', 'Standard camphor tablets for daily use.', 1, 'Product', 4, NOW(), NOW()),
(1012, 1, 'Camphor Cubes', 'Strong aromatic camphor cubes.', 1, 'Product', 4, NOW(), NOW()),

-- Dhoop Sticks (5)
(1013, 1, 'Loban Dhoop', 'Loban-scented dhoop sticks.', 1, 'Product', 5, NOW(), NOW()),
(1014, 1, 'Guggul Dhoop', 'Fragrant guggul dhoop for havan.', 1, 'Product', 5, NOW(), NOW()),
(1015, 1, 'Rose Dhoop', 'Rose-scented dhoop for divine fragrance.', 1, 'Product', 5, NOW(), NOW());


INSERT INTO catalog_attribute (
    id,
    catalog_attribute_key,
    category_id,
    sub_category_id,
    attribute_datatype_id,
    label,
    attribute_group_id,
    is_custom,
    is_required,
    is_filterable,
    is_summary,
    sort_order,
    attribute_icon_id,
    created_at,
    updated_at,
    allowed_values_source
) VALUES
(1, 'brand', 101, 1, 1, 'Brand', 1, 0, 0, 1, 1, 1, NULL, NOW(), NOW(), 'brands'),
(2, 'color', 101, 1, 2, 'Color', 1, 0, 0, 1, 1, 5, NULL, NOW(), NOW(), 'colors'),
(3, 'size', 101, 1, 2, 'Size', 2, 0, 0, 1, 1, 5, NULL, NOW(), NOW(), 'size'),
(4, 'storage', 101, 1, 2, 'Storage', 2, 0, 0, 1, 1, 5, NULL, NOW(), NOW(), 'storage'),
(5, 'price', 101, 1, 4, 'Price', 1, 1, 1, 1, 1, 2, NULL, NOW(), NOW(), NULL);



INSERT INTO attribute_allowed_value (
  id,
  attribute_id,
  value,
  sort_order,
  created_at
) VALUES
-- Brand (attribute_id = 1)
(1, 1, 'Apple', 1, NOW()),
(2, 1, 'Samsung', 2, NOW()),
(3, 1, 'Nokia', 3, NOW()),

-- Color (attribute_id = 2)
(4, 2, 'Red', 1, NOW()),
(5, 2, 'Space Gray', 2, NOW()),
(6, 2, 'Silver', 3, NOW()),

-- Size (attribute_id = 3)
(7, 3, 'Pro', 1, NOW()),
(8, 3, 'Pro Plus', 2, NOW()),
(9, 3, 'Pro Max', 3, NOW()),

-- Storage (attribute_id = 4)
(10, 4, '32GB', 1, NOW()),
(11, 4, '64GB', 2, NOW()),
(12, 4, '128GB', 3, NOW());



--------------------------------------------------------Catalog-------------------------------------------------------


SET FOREIGN_KEY_CHECKS = 0;
DELETE FROM category_master;
SET FOREIGN_KEY_CHECKS = 1;

INSERT INTO category_master 
(id, parent_id, name, category_type, description, display_order, image_url, is_active, created_at, updated_at)
VALUES
(1, NULL, 'Product', 'Product', 'All tangible products', 1, 'assets/images/placeholder.png', 1, NOW(), NOW()),
(2, NULL, 'Service', 'Service', 'All service-based offerings', 2, 'assets/images/placeholder.png', 1, NOW(), NOW()),
(3, NULL, 'Resource', 'Resource', 'All human or spiritual resources', 3, 'assets/images/placeholder.png', 1, NOW(), NOW());

INSERT INTO category_master 
(id, parent_id, name, category_type, description, display_order, image_url, is_active, created_at, updated_at)
VALUES
-- Product Subcategories
(101, 1, 'Electronics', 'Product', 'Electronic items like phones and gadgets', 1, 'assets/images/placeholder.png', 1, NOW(), NOW()),
(102, 1, 'Clothing', 'Product', 'Men and women clothing', 2, 'assets/images/placeholder.png', 1, NOW(), NOW()),
(103, 1, 'Fitness', 'Product', 'Fitness equipment, accessories, and activewear', 3, 'assets/images/placeholder.png', 1, NOW(), NOW()),

-- Service Subcategories
(104, 2, 'Pooja', 'Service', 'Religious pooja services and rituals', 1, 'assets/images/placeholder.png', 1, NOW(), NOW()),
(105, 2, 'Temple', 'Service', 'Religious temples and associated services', 2, 'assets/images/placeholder.png', 1, NOW(), NOW()),
(106, 2, 'Astrologer', 'Service', 'Astrology services and consultations', 3, 'assets/images/placeholder.png', 1, NOW(), NOW()),
(107, 2, 'Kathavachak', 'Service', 'Religious storytellers and spiritual discourses', 4, 'assets/images/placeholder.png', 1, NOW(), NOW()),


SET FOREIGN_KEY_CHECKS = 0;
DELETE FROM subcategory_master;
SET FOREIGN_KEY_CHECKS = 1;

INSERT INTO subcategory_master (
    id, category_id, name, description, is_active, parent_id, created_at, updated_at
) VALUES
-- Electronics
(1, 101, 'Mobile Phones', 'Smartphones and mobile devices', 1, NULL, NOW(), NOW()),
(2, 101, 'Laptops', 'Personal and professional laptops', 1, NULL, NOW(), NOW()),
(3, 101, 'Apple', 'Apple brand smartphones', 1, 1, NOW(), NOW()),
(4, 101, 'Samsung', 'Samsung brand smartphones', 1, 1, NOW(), NOW()),

-- Clothing
(5, 102, 'Men', 'Men’s clothing collection', 1, NULL, NOW(), NOW()),
(6, 102, 'Women', 'Women’s clothing collection', 1, NULL, NOW(), NOW()),
(7, 102, 'Jeans', 'Men’s jeans', 1, 5, NOW(), NOW()),
(8, 102, 'T-Shirts', 'Casual t-shirts for men', 1, 5, NOW(), NOW()),

-- Fitness
(9, 103, 'Equipment', 'Fitness and gym equipment', 1, NULL, NOW(), NOW()),
(10, 103, 'Yoga Mat', 'High-quality yoga mats for workouts and yoga sessions', 1, 9, NOW(), NOW()),

-- Pooja (Service)
(11, 104, 'Ganesh Pooja', 'Ganesh Chaturthi pooja and rituals', 1, NULL, NOW(), NOW()),
(12, 104, 'Lakshmi Pooja', 'Pooja for Goddess Lakshmi prosperity blessings', 1, NULL, NOW(), NOW()),
(13, 104, 'Ganesh Chaturthi', 'Special Ganesh Chaturthi pooja event', 1, 11, NOW(), NOW()),

-- Temple (Service)
(14, 105, 'Vishnu Temple', 'Temples dedicated to Lord Vishnu', 1, NULL, NOW(), NOW()),
(15, 105, 'Shiva Temple', 'Temples dedicated to Lord Shiva', 1, NULL, NOW(), NOW()),
(16, 105, 'Tirupati Temple', 'Famous Vishnu temple located in Tirupati', 1, 14, NOW(), NOW()),

-- Astrologer (Service)
(17, 106, 'Vedic Astrology', 'Personal horoscope analysis', 1, NULL, NOW(), NOW()),
(18, 106, 'Numerology', 'Future predictions through palm analysis', 1, NULL, NOW(), NOW()),

-- Kathavachak (Service)
(19, 107, 'Spiritual Kathavachak', 'Spiritual storytelling sessions', 1, NULL, NOW(), NOW()),
(20, 107, 'Ramayana Kathavachak', 'Kathas from Ramayana', 1, 19, NOW(), NOW()),
(21, 107, 'Bhagavad Gita Kathavachak', 'Teachings from Bhagavad Gita', 1, 19, NOW(), NOW()),

(22, 106, 'Horoscope Reading', 'Personal horoscope analysis', 1, 17, NOW(), NOW()),
(23, 106, 'Name Analysis', 'Future predictions through palm analysis', 1, 18, NOW(), NOW());

