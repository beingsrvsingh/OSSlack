use addressdb;

INSERT INTO address_type (id, name, description)
VALUES
(1, 'User', 'Represents a general user address'),
(2, 'Partner', 'Address associated with a business partner'),
(3, 'Temple', 'Address of a temple location'),
(4, 'Priest', 'Address associated with a priest'),
(5, 'Astrologer', 'Address associated with an astrologer'),
(6, 'Vendor', 'Vendor''s registered address'),
(7, 'DeliveryPartner', 'Address used by delivery personnel or partners'),
(8, 'Office', 'Office or workplace address'),
(9, 'Home', 'Personal or residential address'),
(10, 'Other', 'Any other type of address');


INSERT INTO address (
    uid, owner_id, owner_type, name, label,
    address_line_1, address_line_2, city, state, country, pincode,
    landmark, phone_number, address_type_id, is_default, is_active,
    latitude, longitude, time_zone, created_by, updated_by, created_at, updated_at
)
VALUES
(
    UUID(),                       -- unique UID
    1001,                         -- OwnerId
    0,                            -- OwnerType = User
    'Saurav Sinha',
    'Home',
    'Gate No.7 7005 - A FirstFloor',
    'Kailash Colony',
    'New Delhi',
    'Delhi',
    'India',
    '110066',
    'Near Main Market',
    '981806003',
    9,                            -- AddressType = Home
    TRUE,                         -- IsDefault
    TRUE,                         -- IsActive
    NULL,                         -- Latitude
    NULL,                         -- Longitude
    'Asia/Kolkata',               -- TimeZone
    1,                            -- CreatedBy
    1,                            -- UpdatedBy
    NOW(6),
    NOW(6)
),
(
    UUID(),
    1001,
    0,
    'Jaksh Sinha',
    'Work',
    'Gate No.9007 - A Fourth',
    'East of Kailash',
    'Lajpat Nagar',
    'Delhi',
    'India',
    '110055',
    'Near Metro Station',
    '801806995',
    8,                            -- AddressType = Office
    FALSE,
    TRUE,
    NULL,
    NULL,
    'Asia/Kolkata',
    1,
    1,
    NOW(6),
    NOW(6)
);



select * from address;
select * from address_type;


