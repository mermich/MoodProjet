
# Lancer le projet serveur
Ouvrir la solution `MoodProjet.sln` et lancer le programme en debug.
## Connexion a la base de donnees
Avant tout editer la chaine de connexion a la base de donnees. Elle se trouve dans le fichier launchSettings.json

## Initialisation de la base de donnees
Une fois lance plusieurs services (endpoints) sont disponibles, notamment :
 - Init-CheckDB: [GET] http://localhost:7120/api/CheckDB => permet de tester la chaine de connexion a la base de donnees.
 - Init-KickStart: [POST] http://localhost:7120/api/KickStart => permet de lancer les scripts necessaires a la creation des tables, alimentation et insertion de donnees fictives.


## Tester les services
Pour tester les differents services il est recommande d'utiliser postman (https://www.postman.com/).
La collection postman des differents services peut etre importe a partir du fichier : `Mood Project.postman_collection.json` .



# Lancer l'application cliente (Angular)
Ouvrir le dossier MoodWeb avec visual studio code, puis lancer la commande `ng serve` dans le terminal :
√ Compiled successfully.

Ouvir un navigateur et aller sur la page : http://localhost:4200/
L'ecran de saisie de mood devrai s'afficher.