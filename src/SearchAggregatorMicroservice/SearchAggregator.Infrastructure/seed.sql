use searchdb;

INSERT INTO user_search_history 
    (user_id, query, platform, language, ip_address, result_count, searched_at)
VALUES
    ('user123', 'brass puja bell', 'Web', 'en', '192.168.1.10', 10, NOW()),
    ('user456', 'sandalwood incense sticks', 'Android', 'en', '192.168.1.11', 5, NOW()),
    ('user789', 'incense sticks', 'iOS', 'fr', '2001:0db8:85a3:0000:0000:8a2e:0370:7334', 7, NOW()),
    ('user123', 'puja items', 'Web', 'en', '192.168.1.10', 15, NOW()),
    ('user456', 'fragrance sticks', 'Android', 'en', '192.168.1.11', 3, NOW());
