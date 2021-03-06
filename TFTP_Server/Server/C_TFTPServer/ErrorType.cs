using System.Net;
using System.Net.Sockets;

namespace Server.C_TFTP
{
    class ErrorType
    {
        protected string DetectionTypeErreur(Socket s, EndPoint pointDistant, int code)
        {
            // Tableaux de bytes renfermant les trames
            byte[] Trame = new byte[512];
            // Message d'erreur correspondant au type d'erreur
            string MessageErreur = ((ErrorCode)code).ToString();

            // Variables d'incrémentation
            int indice, iMessage = 0;

            // C/ration de la trame d'erreur à envoyer au client
            Trame[0] = 0;
            Trame[1] = 5;
            Trame[2] = 0;
            Trame[3] = (byte)code;

            // Choix du message selon le type de problème
            for (indice = 0; indice < MessageErreur.Length; indice++)
            {
                Trame[indice + 4] = (byte)MessageErreur[iMessage];
                iMessage++;
            }
            // Envoi du type d'erreur au serveur / client
            Trame[indice] = 0;
            s.SendTo(Trame, pointDistant);
            // Envoyer le statut du serveur au formulaire
            return string.Format("Une erreur a été rencontrée : {0}\r\n", MessageErreur);
        }
    }
}
