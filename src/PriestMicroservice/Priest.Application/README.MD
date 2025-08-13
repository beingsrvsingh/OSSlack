| Id | UserId         | DisplayName      | ProfilePictureUrl                                                                                  | AverageRating | TotalRatings | IsActive | CreatedAt           | UpdatedAt           | PriestExpertise |
| -- | -------------- | ---------------- | -------------------------------------------------------------------------------------------------- | ------------- | ------------ | -------- | ------------------- | ------------------- | --------------- |
| 1  | user-1234-uuid | Pandit Ram Kumar | [https://example.com/images/priests/ramkumar.jpg](https://example.com/images/priests/ramkumar.jpg) | 4.75          | 120          | true     | 2024-08-01 10:00:00 | 2025-07-15 14:30:00 | VedicRituals    |
| 2  | user-5678-uuid | Pandit Shyam     | [https://example.com/images/priests/shyam.jpg](https://example.com/images/priests/shyam.jpg)       | 4.90          | 95           | true     | 2023-12-05 09:00:00 | 2025-01-10 11:00:00 | YajurVeda       |


| Id | PriestId | Language |
| -- | -------- | -------- |
| 1  | 1        | Hindi    |
| 2  | 1        | Sanskrit |
| 3  | 2        | Hindi    |
| 4  | 2        | Tamil    |


| ScheduleId | PriestId | Date       | IsAvailable |
| ---------- | -------- | ---------- | ----------- |
| 1          | 1        | 2025-08-15 | true        |
| 2          | 1        | 2025-08-16 | false       |
| 3          | 2        | 2025-08-15 | true        |


| TimeSlotId | ScheduleId | StartTime | EndTime | IsBooked |
| ---------- | ---------- | --------- | ------- | -------- |
| 1          | 1          | 09:00     | 10:00   | false    |
| 2          | 1          | 10:00     | 11:00   | true     |
| 3          | 3          | 14:00     | 15:00   | false    |


| PackageId | PriestId | Name               | Description                                    | Price   | DurationInMinutes |
| --------- | -------- | ------------------ | ---------------------------------------------- | ------- | ----------------- |
| 1         | 1        | Ganesh Puja Ritual | Complete Ganesh Puja with materials & chanting | 1500.00 | 90                |
| 2         | 2        | Navagraha Pooja    | Ritual for planetary peace and blessings       | 1200.00 | 75                |
