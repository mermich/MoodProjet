
# Pour Creer le projet Angular
Ouvrir visual studio code et lancer la commande suivante :
` ng new MoodWeb --style=css --minimal --prefix=mood --routing=true `

# Pour lancer l'application web
Se placer de le reprtoir de l'application web 
` cd .\MoodWeb\   `
Puis lancer le serveur
` ng serve `


# Informations supplementaires pour la couche serveur
## Chaine de connexion a la base de donnees
Editer DbHelper.cs et corriger la chaine de connection pour pointer vers votre base de donnees:
` "Server=127.0.0.1;Port=3306;Database=moodDb;Uid=admin;Pwd=admin;" `

## Cors
Si, interroger le serveur depuis Postman marche correctement mais pas depuis le projet Angular, il y a de fortes chanques que ce soit lie a CORS (https://developer.mozilla.org/fr/docs/Web/HTTP/CORS)
Editer le fichier local.settings.json :
` {
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  },
  "Host": {
    "CORS": "*",
    "CORSCredentials": false
  }
} `

Editer launchSettings.json
` {
  "profiles": {
    "MoodProjet": {
      "commandName": "Project",
      "commandLineArgs": "--port 7120 --cors *",
      "launchBrowser": false
    }
  } 
} `

bien noter l'arguments --cors * qui autorize toutes les adresses a consulter cette ressource.