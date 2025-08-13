| Id | Name                   | Description                            | ImageUrl                          | IsActive | CreatedAt            | UpdatedAt | LocationId |
| -- | ---------------------- | -------------------------------------- | --------------------------------- | -------- | -------------------- | --------- | ---------- |
| 1  | Shri Siddhivinayak     | Famous Ganesh temple in Mumbai         | /images/temples/siddhivinayak.jpg | true     | 2025-08-13T08:00:00Z | null      | 101        |
| 2  | Kashi Vishwanath       | Sacred Shiva temple in Varanasi        | /images/temples/kashi.jpg         | true     | 2025-08-13T08:05:00Z | null      | 102        |
| 3  | Meenakshi Amman Temple | Historic temple in Madurai             | /images/temples/meenakshi.jpg     | true     | 2025-08-13T08:10:00Z | null      | 103        |
| 4  | Golden Temple          | Major Sikh pilgrimage site in Amritsar | /images/temples/golden.jpg        | true     | 2025-08-13T08:15:00Z | null      | 104        |
| 5  | ISKCON Bangalore       | Popular Krishna temple with rituals    | /images/temples/iskcon.jpg        | true     | 2025-08-13T08:20:00Z | null      | 105        |


| Id | TempleId | Name            | Description          | Price | DurationMins |
| -- | -------- | --------------- | -------------------- | ----- | ------------ |
| 1  | 1        | Ganesh Abhishek | Special Ganesh pooja | 500   | 30           |
| 2  | 2        | Rudra Abhishek  | Shiva pooja          | 1000  | 45           |
| 3  | 3        | Meenakshi Pooja | Daily goddess pooja  | 700   | 25           |


| Id | TempleId | Name              | Description              | SuggestedAmount |
| -- | -------- | ----------------- | ------------------------ | --------------- |
| 1  | 1        | Annadanam         | Food for devotees        | 100             |
| 2  | 2        | Temple Renovation | Donation for restoration | 500             |


| Id | TempleId | Name         | Description         | Price |
| -- | -------- | ------------ | ------------------- | ----- |
| 1  | 1        | Modak Pack   | Sweet offering      | 50    |
| 2  | 3        | Laddu Prasad | Famous temple laddu | 40    |


| Id | TempleId | Name          | Time     |
| -- | -------- | ------------- | -------- |
| 1  | 1        | Mangala Aarti | 06:00 AM |
| 2  | 1        | Sandhya Aarti | 07:00 PM |
| 3  | 2        | Shiv Aarti    | 06:30 AM |


| Id | TempleId | DayOfWeek | OpenTime | CloseTime |
| -- | -------- | --------- | -------- | --------- |
| 1  | 1        | Monday    | 05:30    | 21:00     |
| 2  | 1        | Sunday    | 06:00    | 22:00     |
| 3  | 2        | All Days  | 04:00    | 23:00     |


| Id | TempleId | Date       | Reason           |
| -- | -------- | ---------- | ---------------- |
| 1  | 1        | 2025-09-01 | Ganesh Chaturthi |
| 2  | 2        | 2025-10-24 | Deepavali        |


| Id | TempleId | LanguageCode | LocalizedName          | LocalizedDescription           |
| -- | -------- | ------------ | ---------------------- | ------------------------------ |
| 1  | 1        | hi           | श्री सिद्धिविनायक      | मुंबई का प्रसिद्ध गणेश मंदिर   |
| 2  | 2        | ta           | காசி விஸ்வநாதர் கோவில் | வராணாசியின் புனித சிவன் கோவில் |
