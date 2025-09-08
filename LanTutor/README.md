LanTutor-Language Tutoring Application

Overview

LanTutor is a C# application designed to help users learn new languages. It provides a graphical user interface (GUI) built with GTK# and utilizes a SQLite database to store words, translations, definitions, and user progress.  The application supports multiple learning modes and allows users to track their progress over time.
It uses an adapter pattern to allow different data sources to be used for the words.
LanTutor employs a spaced repetition system with a priority bias within its tutoring style, aiming to show users words they struggle with more frequently while tracking scores and attempts to measure learning progress. Active recall modes are implemented via tests that the user can use to see where they are.

Features

    • Multiple Learning Modes:** Practice, Word Test, Description Test (partially implemented)
    • Language Selection:** Choose from available language dictionaries.
    • Score Tracking:** Monitor progress and identify areas for improvement.
    • Session Management:** Track learning sessions.
    • Data Storage:** Supports XML and SQLite data storage.
    • User Settings:** Persists user preferences for language and learning mode.
    • TON Wallet Integration:** Basic integration with TON wallet for authentication (work in progress).

Dependencies

    • .NET Framework 4.7
    • GTK#
    • Microsoft.Data.Sqlite
    • EntityFramework
    • Newtonsoft.Json
    • TonSdk.Core