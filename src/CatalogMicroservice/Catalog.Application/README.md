| CategoryId | Name                      | Description                                                    | IsActive |
| ---------- | ------------------------- | -------------------------------------------------------------- | -------- |
| 1          | Temple Pooja              | Poojas and services performed in temples                       | true     |
| 2          | Home Pooja                | Poojas and rituals performed at home                           | true     |
| 3          | Astrologer                | Astrology and related services                                 | true     |
| 4          | Pooja Essentials          | Individual items used for pooja (incense, diyas, kumkum, etc.) | true     |
| 5          | Samagri Kits              | Pre-packed kits for specific poojas                            | true     |
| 6          | Spiritual Books & Yantras | Books, yantras, and spiritual literature                       | true     |
| 7          | Decor & Idols             | Decorative items and idols for pooja                           | true     |
| 8          | Ritual Accessories        | Miscellaneous ritual accessories                               | true     |


| SubcategoryId | CategoryId | Name             | ParentSubcategoryId | Type     | Notes                              |
| ------------- | ---------- | ---------------- | ------------------- | -------- | ---------------------------------- |
| 100           | 1          | Aarti            | NULL                | Service  | Parent for temple Aarti types      |
| 101           | 1          | Donation         | NULL                | Service  | Parent for temple donation types   |
| 102           | 1          | Prasad           | NULL                | Service  | Parent for temple prasad offerings |
| 103           | 1          | Pooja            | NULL                | Service  | Parent for temple poojas           |
| 104           | 1          | Bhajan/Kirtan    | NULL                | Service  | Parent for devotional music events |
| 105           | 1          | Bhandara         | NULL                | Service  | Parent for community meals         |
| 110           | 1          | Mangala Aarti    | 100                 | Aarti    | Morning Aarti                      |
| 111           | 1          | Sandhya Aarti    | 100                 | Aarti    | Evening Aarti                      |
| 120           | 1          | General Donation | 101                 | Donation | Open donation                      |
| 121           | 1          | Annadanam        | 101                 | Donation | Food donation                      |
| 130           | 1          | Prasad Box       | 102                 | Prasad   | Packaged prasad                    |
| 140           | 1          | Rudra Abhishek   | 103                 | Pooja    | Temple Rudra Pooja                 |
| 141           | 1          | Ganesh Pooja     | 103                 | Pooja    | Temple Ganesh Pooja                |
| 150           | 1          | Satsang Bhajan   | 104                 | Bhajan   | Community bhajan                   |
| 151           | 1          | Kirtan Night     | 104                 | Bhajan   | Kirtan by invited musicians        |
| 160           | 1          | Sponsor Bhandara | 105                 | Bhandara | User sponsors bhandara             |
| 161           | 1          | Attend Bhandara  | 105                 | Bhandara | Participation info                 |
| SubcategoryId | CategoryId | Name               | ParentSubcategoryId | Type    | Notes                         |
| ------------- | ---------- | ------------------ | ------------------- | ------- | ----------------------------- |
| 200           | 2          | Home Pooja Rituals | NULL                | Service | Parent for home pooja rituals |
| 201           | 2          | Ganesh Pooja       | 200                 | Pooja   | Home Ganesh Pooja             |
| 202           | 2          | Navagraha Pooja    | 200                 | Pooja   | Home Navagraha Pooja          |
| 203           | 2          | Satyanarayan Pooja | 200                 | Pooja   | Home Satyanarayan Pooja       |
| SubcategoryId | CategoryId | Name                                   | ParentSubcategoryId | Type      | Notes                         |
| ------------- | ---------- | -------------------------------------- | ------------------- | --------- | ----------------------------- |
| 300           | 3          | Horoscope & Kundli                     | NULL                | Astrology | Kundli and horoscope services |
| 301           | 3          | Vaastu Consultation                    | NULL                | Astrology | Vaastu related consultation   |
| 302           | 3          | Career / Health / Finance Consultation | NULL                | Astrology | Personalized consultancy      |
| 303           | 3          | Palmistry & Tarot Reading              | NULL                | Astrology | Palm and tarot readings       |
| 304           | 3          | Mind Reading & Spiritual Guidance      | NULL                | Astrology | Spiritual guidance            |
| SubcategoryId | CategoryId | Name                   | ParentSubcategoryId | Type    | Notes                         |
| ------------- | ---------- | ---------------------- | ------------------- | ------- | ----------------------------- |
| 400           | 4          | Incense Sticks         | NULL                | Product | Agarbatti and incense sticks  |
| 401           | 4          | Diyas & Lamps          | NULL                | Product | Traditional oil lamps         |
| 402           | 4          | Flowers                | NULL                | Product | Fresh flowers for pooja       |
| 403           | 4          | Kumkum, Haldi, Chandan | NULL                | Product | Holy powders                  |
| 404           | 4          | Camphor                | NULL                | Product | Camphor for rituals           |
| 405           | 4          | Pooja Utensils         | NULL                | Product | Plates, bowls, small utensils |
| SubcategoryId | CategoryId | Name                | ParentSubcategoryId | Type | Notes                       |
| ------------- | ---------- | ------------------- | ------------------- | ---- | --------------------------- |
| 500           | 5          | Ganesh Pooja Kit    | NULL                | Kit  | Pre-packed Ganesh pooja kit |
| 501           | 5          | Navagraha Pooja Kit | NULL                | Kit  | Pre-packed Navagraha kit    |
| SubcategoryId | CategoryId | Name            | ParentSubcategoryId | Type    | Notes                         |
| ------------- | ---------- | --------------- | ------------------- | ------- | ----------------------------- |
| 600           | 6          | Spiritual Books | NULL                | Product | Religious and spiritual books |
| 601           | 6          | Yantras         | NULL                | Product | Sacred geometric diagrams     |
| SubcategoryId | CategoryId | Name             | ParentSubcategoryId | Type    | Notes                |
| ------------- | ---------- | ---------------- | ------------------- | ------- | -------------------- |
| 700           | 7          | Idols            | NULL                | Product | Idols of deities     |
| 701           | 7          | Decorative Items | NULL                | Product | Decoration for pooja |
| SubcategoryId | CategoryId | Name               | ParentSubcategoryId | Type    | Notes                           |
| ------------- | ---------- | ------------------ | ------------------- | ------- | ------------------------------- |
| 800           | 8          | Ritual Accessories | NULL                | Product | Miscellaneous pooja accessories |



| KitItemId | KitSubcategoryId | ProductSubcategoryId | Notes           |
| --------- | ---------------- | -------------------- | --------------- |
| 1         | 501              | 400                  |                 |
| 2         | 501              | 402                  |                 |
| 3         | 501              | 403                  |                 |
| 4         | 502              | 401                  |                 |
| 5         | 502              | 402                  |                 |
| 6         | 502              | 403                  |                 |
| 7         | 502              | 404                  | Decorative item |
