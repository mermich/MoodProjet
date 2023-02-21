Mood web est un projet de demonstration d'application simple page. 
il est constititue d'une solution c# qui sert de serveur et d'une application Angular pour le client.
Le donnees seront persistee dans une base de donnees MySql.

# Récuperation des sources
Dans github, depuis le bouton vert `sources`  cloner le repository ou telecharger le zip (et le dezipper).

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
Ouvrir le dossier MoodWeb avec visual studio code, puis lancer les commandes dans le terminal : 
## Packages manquants
`npm install` => cela va installer les packages npm requis pour l'application, suivant les packages deja installes la sortie du terminal devrai ressembler a (si il y a des warning ce n'est pas grave):
`added 896 packages, and audited 897 packages in 32s
86 packages are looking for funding
  run npm fund for details
found 0 vulnerabilities`

## Lancer le serveur
`ng serve`  => cela va lancer la compilation du projet et lancer l'application web, si tous se passe bien le terminal devrai afficher :

`** Angular Live Development Server is listening on localhost:4200, open your browser on http://localhost:4200/ **
√ Compiled successfully.`

Ouvrir un navigateur vers la page : http://localhost:4200/
