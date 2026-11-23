# Frosty Targets – 3AHMNS

Unity-Version: **6000.3.24f1**. Projekt: Frosty Targets.

## Projekt öffnen
1. Unity Hub öffnen, **Add project from disk** wählen und diesen Ordner auswählen.
2. Unity **6000.3.24f1** verwenden und den ersten Paketimport abwarten.
3. Szene `Assets/MyGame/Scenes/ShootingScene.unity` öffnen, **Play** drücken.
4. `Assets/MyGame/Scripts/GameManager.cs` und `Target.cs` untersuchen.

## Hinweis bei `UnityEngine.UI` / `Text`-Fehlern
Das Paket **Unity UI (uGUI)** ist unter `Packages/manifest.json` als `com.unity.ugui` eingetragen. Falls trotzdem CS0234 oder CS0246 erscheint: `Window > Package Management > Package Manager` öffnen und unter **In Project** prüfen, ob Unity UI geladen ist. Im Paketmanager **Refresh/Resolve** ausführen; gegebenenfalls Unity schließen und den Ordner `Library` löschen, dann erneut öffnen. Nicht die `using UnityEngine.UI`-Zeile entfernen: sie wird für die vorhandene Punkteanzeige gebraucht.

## Unterricht
Das vorhandene Spawning, der Score und der Sieg bei zehn Treffern sind **Ausgangscode**. Schüler:innen erweitern diese Mechaniken anhand der GitHub-Issues. Der Erstimport ist eine Ausgangsversion; keine Aufgaben sind vorweg implementiert.

## Ablauf
Erzeugen → Anklicken → Punkte → Spielende:
Der GameManager spawnt jede Sekunde eine Zielscheibe an einer zufälligen Position. Klickt der Spieler sie an, wird sie zerstört und er bekommt einen Punkt. Bei 10 Punkten ist das Spiel gewonnen.

## GameManager.cs
- `Start()`: Startet mit `InvokeRepeating` das wiederholte Aufrufen von `Spawn()` (erstmals nach 1 s, dann jede Sekunde).
- `Update()`: Läuft jedes Frame. Spielt bei jedem Linksklick den Schussound ab und bricht das Spawnen ab, sobald `win` true ist.
- `Spawn()`: Instanziert das Target-Prefab als Kind von `parentOfTargets` und setzt es per `localPosition` auf einen zufälligen x/y-Wert im Spielfeld.
- `IncrementScore()`: Erhöht `score` um 1, aktualisiert `scoreText` und setzt bei 10 Punkten `win = true` und zeigt `winSprite`.

## Target.cs
- `Start()`: Holt sich per `GameObject.Find` den GameManager und löscht sich mit `Destroy(gameObject, 2f)` nach 2 Sekunden selbst.
- `OnMouseDown()`: Wird beim Anklicken ausgeführt. Ruft `IncrementScore()` im GameManager auf und zerstört die Scheibe sofort.

## Bereits vorhanden
- **Zufallsposition**: `Random.Range` für x und y in `Spawn()`.
- **Punkteanzeige**: `scoreText` wird bei jedem Treffer aktualisiert.
- **Siegbedingung**: `score >= 10` beendet das Spiel und blendet das Gewinn-Bild ein.

## Geplante Änderung 
Zieltypen mit unterschiedlichen Punktewerten