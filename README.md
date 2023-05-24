# Aplikacja do gromadzenia i archiwizacji dokumentów elektronicznych.

Celem aplikacji jest gromadzenie i przechowywani dokumentów w postaci dowolnych plików.

Każdy dokument zawiera metadane opisujące treść dokumentu: np. tytuł, typ pliku, opis zawartości, data utworzenia, kto go wprowadził do systemu oraz status udostępniania - publiczny, prywatny, poufny.

> **Publiczny** – każdy użytkownik (także nie zarejestrowany w systemie) może wyszukać lub pobrać dokument.

> **Prywatny** – tylko zarejestrowani użytkownicy mogą wyszukiwać lub pobierać taki dokument.

> **Poufny** – tylko użytkownicy o prawach typu Admin mogą wyszukiwać lub pobierać oraz ci, którzy ten dokument dodali do systemu.

# Operacje
- Zarchiwizowanie (dodanie) dokumentu z opisem – każdy użytkownik może dodać dokument do systemu, który ma status publiczny. Użytkownicy zarejestrowani mogą dodawać dokumenty o dowolnym statusie.

- Odczyt i wyszukiwanie – użytkownicy niezarejestrowani mogą wyszukiwać i pobierać tylko dokumentu publiczne. Jeśli kryterium wyszukiwania pasuje do dokumentu niepublicznego to w wynikach wyszukiwanie nie powinien by ujawniany (użytkownik nie może nawet wiedzieć, że taki istnieje).

- Użytkownicy zarejestrowani o prawach oprócz Admi, mogą wyszukiwać i odczytywać dokumenty publiczne, prywatne oraz te poufne, które ten użytkownik dodał do systemu.

- Użytkownicy Admin maja prawo do wyszukiwania, pobierania, usuwania oraz modyfikacji opisu wszystkich dokumentów. Każda operacja edycji, pobrania, dodania musi być rejestrowana z informacjami: kto i kiedy wykonał daną operację na danym dokumencie. Historia wszystkich operacji wykonanych na dokumencie jest widoczna tylko dla Admin’ów.

- Każdy z użytkowników zarejestrowanych może dostać listę dokumentów, które sam wprowadził do systemu.

```mermaid
classDiagram
  class EntityBase {
    <<Abstract>>
    +Id : long
    +CreationDate : DateTime
  }

  class User {
    +Login : string
    +Password : string
    +Role : UserRole
  }

    class UserRole {
    «enumeration»
    User,
    Admin
  }

  class Document {
    +Title : string
    +Description : string
    +Author : string
    +DocumentType : DocumentAccessStatus
    +Path : string
  }

    class DocumentAccessStatus {
    «enumeration»
    Public,
    Private,
    Confidential
  }

  class Log {
    +Id : long
    +CreationDate : DateTime
    +LogType : ActionLog
    +Author : User
    +Document : Document
  }

class ProjectContext {
    +Documents : DbSet<Document>
    +Users : DbSet<User>
    +Logs : DbSet<Log>
    +ProjectContext(options: DbContextOptions<ProjectContext>)
    +OnConfiguring(optionsBuilder: DbContextOptionsBuilder)
  }

  class AuthenticationService {
    +Login(username: string, password: string): string
    +Register(username: string, password: string): void
    +Logout(): void
  }

  class AuthorizationService {
    +IsUserAuthorized(user: User, document: Document): bool
    +IsAdmin(user: User): bool
  }

  class AuditService {
    +LogAction(user: User, action: string, document: Document): void
  }

  class HashingService {
    +HashPassword(password: string): string
    +VerifyPassword(password: string, hashedPassword: string): bool
  }

  class UserRepository {
    +GetUserById(id: long): User
    +GetUserByLogin(login: string): User
    +AddUser(user: User): void
    +UpdateUser(user: User): void
    +DeleteUser(user: User): void
  }

  class DocumentRepository {
    +GetDocumentById(id: long): Document
    +GetDocumentsByAuthor(author: string): List<Document>
    +AddDocument(document: Document): void
    +UpdateDocument(document: Document): void
    +DeleteDocument(document: Document): void
  }

  class LogRepository {
    +GetLogById(logId: long): Log
    +AddLog(log: Log): void
    +UpdateLog(log: Log): void
    +DeleteLog(log: Log): void
  }

  class LogService {
    -_logRepository: LogRepository
    +CreateLog(logType: ActionLog, author: User, document: Document): Log
    +UpdateLog(log: Log): void
    +DeleteLog(log: Log): void
  }
  class ActionLog  {
    «enumeration»
    Upload,
    Edit,
    Download
  }

  EntityBase <|-- User
  EntityBase <|-- Document
  EntityBase <|-- Log

  User --o UserRole

  Document --o DocumentAccessStatus
  Document --o User

  Log --o ActionLog
  Log --o Document
 
  AuthenticationService --* UserRepository : uses
  AuthenticationService --* HashingService : uses

  AuthorizationService --* UserRepository : uses
  AuthorizationService --* DocumentRepository : uses

  AuditService --*  UserRepository : uses
  AuditService --*  DocumentRepository : uses

  LogService --|> LogRepository : uses
  LogService --|> UserRepository  : uses
  LogService --|> DocumentRepository : uses

  UserRepository --|> EntityBase
  DocumentRepository --|> EntityBase
  LogRepository --|> EntityBase

  ProjectContext o--> Document
  ProjectContext o--> User
  ProjectContext o--> Log

```
#
Dodatkowe funkcje ponoszące ocenę z projektu: Pobieranie skompresowanej paczki dokumentów np. aplikacja korzysta z serwisu gRCP, który wykonuje kompresję plików, umieszcza plik wynikowy na serwerze i zwraca link do archiwum.  

# Technologie użyte w projekcie
- [Microsoft.AspNet.Mvc](https://www.nuget.org/packages/Microsoft.AspNet.Mvc/5.2.9?_src=template)
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/7.0.5?_src=template)
- [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)

# Autorzy
- [Krystian Żywiec **kZywiec**](https://github.com/kZywiec)
- [Mieszko Przybyła **emzetp**](https://github.com/https://github.com/emzetp)
- [Krzysztof Tybinka](https://github.com/KrzysztofTybinka)
- [Krzysztof Nowakowski **knowakowski78**](https://github.com/knowakowski78)
