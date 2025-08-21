# ExamManagementSystem Bu sistem orta məktəbdə oxuyan şagirdlərin aldıqları dərslər üzrə imtahan nəticələrinin qeydiyyatının aparılması üçündür.

## Xüsusiyyətlər
- **Müəllimlərin İdarə Olunması** Müəllim əlavə etmək, redaktə etmək, silmək (Müəllim cədvəlinin ayrı olması normalization məsələləri baxımından daha düzgündür)
- **Siniflərin İdarə Olunması** Sinif əlavə etmək, redaktə etmək, silmək (Sinif cədvəlinin ayrı olması normalization məsələləri baxımından daha düzgündür)
- **Şagirdlərin İdarə Olunması** Şagird əlavə etmək, redaktə etmək, silmək (Sinifi hər dəfə bu cədvələ yazmaq yerinə onun Sinif cədvəlindəki Id - dən bura referans alırıq)
- **Dərslərin İdarə Olunması** Dərs əlavə etmək, redaktə etmək, silmək (Bu cədvələ hər dəfə sinif və müəllimə yazmaq yerinə, onların Id lərini öz cədvəllərindən miras alırıq)
- **İmtahanların İdarə Olunması** İmtahan əlavə etmək, redaktə etmək, silmək (Bu cədvələ hər dəfə dərs və tələbə yazmaq yerinə, onların Id lərini öz cədvəllərindən miras alırıq)

## Texnologiyalar
- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Bootstrap 5
