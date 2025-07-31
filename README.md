# 🌾 Field Service API

🏗 Структура проекта

```text
FieldServiceAPI/
├── 📁 Controllers/       # API контроллеры
├── 📁 Models/            # Модели данных
├── 📁 Services/          # Бизнес-логика
├── 📁 KmlData/           # KML файлы данных
├── 📄 Program.cs         # Конфигурация
└── 📄 appsettings.json   # Настройки
```

## 📋 О проекте

Web API для работы с геопространственными данными полей. Позволяет:

- 📌 Получать информацию о полях
- 📏 Рассчитывать площади
- 🧭 Определять расстояния
- 🔍 Проверять принадлежность точек к полям

## 🛠 Технологии

| Технология | Назначение |
|------------|------------|
| ![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet) | Бэкенд-фреймворк |
| ![NetTopologySuite](https://img.shields.io/badge/NTS-Geo-blue) | Геопространственные расчеты |
| ![Swagger](https://img.shields.io/badge/Swagger-Docs-brightgreen) | Документация API |
| ![Docker](https://img.shields.io/badge/Docker-Container-blue) | Развертывание |

## 🚀 Быстрый старт

### Предварительные требования

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- (Опционально) [Docker](https://www.docker.com/)

```bash
# 1. Клонировать репозиторий
git clone https://github.com/Slavan-ST/AgroholdingEnergomeraLLC.git
cd FieldService/FieldService

# 2. Восстановить зависимости
dotnet restore

# 3. Запустить приложение
dotnet run



После запуска откройте в браузере:
🔗 http://localhost:5000/swagger

