
# Lancer le projet serveur
Ouvrir la solution `MoodProjet.sln` et lancer le programme en debug.

## Connexion à la base de données
Avant tout éditer la chaine de connexion à la base de données. Elle se trouve dans le fichier `launchSettings.json`

## Initialisation de la base de données
Une fois lance plusieurs services (endpoints) sont disponibles, notamment :
 - Init-CheckDB: [GET] http://localhost:7120/api/CheckDB => permet de tester la chaine de connexion à la base de donnees.
 - Init-KickStart: [POST] http://localhost:7120/api/KickStart => permet de lancer les scripts nécessaires à la création des tables, alimentation et insertion de données fictives.

## Tester les services
Pour tester les différents services il est recommandé d'utiliser Postman (https://www.postman.com/).
La collection Postman des différents services peut etre importé à partir du fichier : `Mood Project.postman_collection.json` .


# Lancer l'application cliente (Angular)
Ouvrir le dossier MoodWeb avec visual studio code, puis lancer la commande `ng serve` dans le terminal :
√ Compiled successfully.

Ouvrir un navigateur et aller sur la page : http://localhost:4200/
L'ecran de saisie de mood devrai s'afficher.

