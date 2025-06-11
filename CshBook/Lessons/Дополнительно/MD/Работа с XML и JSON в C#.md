# 📘 Работа с XML и JSON в C\#


## Введение

C# поддерживает мощную работу с форматами XML и JSON, что важно при интеграции с внешними сервисами, хранении настроек и обмене данными.

---

## Работа с XML

### ✅ XML Сериализация / Десериализация

```csharp
using System.Xml.Serialization;
using System.IO;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Сериализация
var person = new Person { Name = "Anton", Age = 18 };
var serializer = new XmlSerializer(typeof(Person));
using var writer = new StreamWriter("person.xml");
serializer.Serialize(writer, person);

// Десериализация
using var reader = new StreamReader("person.xml");
var loaded = (Person)serializer.Deserialize(reader);
Console.WriteLine($"{loaded.Name}, {loaded.Age}");
```

---

### 🧩 XML Атрибуты и Вложенные Элементы

```csharp
[XmlRoot("User")]
public class User
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlElement("FullName")]
    public string Name { get; set; }

    public ContactInfo Contacts { get; set; }

    [XmlArray("Skills")]
    [XmlArrayItem("Skill")]
    public List<string> Skills { get; set; }
}

public class ContactInfo
{
    public string Email { get; set; }
    public string Phone { get; set; }
}
```

**Пример XML:**

```xml
<User id="1">
  <FullName>Anton Aleynichenko</FullName>
  <Contacts>
    <Email>anton@example.com</Email>
    <Phone>+71234567890</Phone>
  </Contacts>
  <Skills>
    <Skill>C#</Skill>
    <Skill>Docker</Skill>
  </Skills>
</User>
```

---

### 🔍 XML вручную

#### XmlDocument

```csharp
var doc = new XmlDocument();
doc.Load("user.xml");

var id = doc.DocumentElement.GetAttribute("id");
var name = doc.SelectSingleNode("//FullName")?.InnerText;
var skills = doc.SelectNodes("//Skill");

foreach (XmlNode skill in skills)
{
    Console.WriteLine(skill.InnerText);
}
```

#### XElement (LINQ to XML)

```csharp
var xml = XElement.Load("user.xml");

string id = xml.Attribute("id")?.Value;
string name = xml.Element("FullName")?.Value;
var skills = xml.Element("Skills")?.Elements("Skill");

foreach (var skill in skills)
{
    Console.WriteLine(skill.Value);
}
```

---

## Работа с JSON

### ✅ JSON Сериализация / Десериализация

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// System.Text.Json
var json1 = JsonSerializer.Serialize(new Person { Name = "Anton", Age = 18 });
var person1 = JsonSerializer.Deserialize<Person>(json1);

// Newtonsoft.Json
var json2 = JsonConvert.SerializeObject(new Person { Name = "Anton", Age = 18 }, Formatting.Indented);
var person2 = JsonConvert.DeserializeObject<Person>(json2);
```

---

### ⚙ JSON Расширенные настройки

```csharp
public class User
{
    [JsonProperty("full_name")]
    public string Name { get; set; }

    [JsonProperty("age_years")]
    public int Age { get; set; }

    [JsonProperty("contacts")]
    public ContactInfo Contact { get; set; }

    [JsonProperty("skills")]
    public List<string> Skills { get; set; }
}

public class ContactInfo
{
    public string Email { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Phone { get; set; }
}
```

**Результат сериализации:**

```json
{
  "full_name": "Anton",
  "age_years": 18,
  "contacts": {
    "Email": "anton@example.com"
  },
  "skills": ["C#", "Docker"]
}
```

---

### 🔍 JSON с dynamic / JObject

```csharp
string json = File.ReadAllText("user.json");

// JObject (Newtonsoft)
var jObj = JObject.Parse(json);
string name = (string)jObj["full_name"];
string email = (string)jObj["contacts"]?["Email"];

foreach (var skill in jObj["skills"])
{
    Console.WriteLine(skill);
}

// dynamic (можно только с Newtonsoft)
dynamic dyn = JsonConvert.DeserializeObject(json);
Console.WriteLine(dyn.full_name);
foreach (var s in dyn.skills)
{
    Console.WriteLine(s);
}
```

---

## Заключение

| Формат | Простой доступ               | Сложные структуры                 | Работа вручную        | Рекомендованная библиотека      |
| ------ | ---------------------------- | --------------------------------- | --------------------- | ------------------------------- |
| XML    | XmlSerializer                | Да (через атрибуты)               | XmlDocument, XElement | `System.Xml`, `System.Xml.Linq` |
| JSON   | JsonSerializer / JsonConvert | Да (вложенные объекты, настройки) | JObject / dynamic     | `Newtonsoft.Json`               |

* `XmlSerializer` хорош для строго типизированных XML, но требует атрибутов.
* `JsonConvert` даёт наибольшую гибкость при работе с JSON (особенно при нестандартной структуре).
* Для ручного разбора документов лучше использовать `XElement` или `JObject`.

