using RimWorld;
using Verse;
using System.Collections.Generic;

namespace CubeeIncident { 
public class IncidentWorker_CubeeRaid : IncidentWorker
{
    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        Map map = (Map)parms.target;

        // Obtenir la richesse de la colonie
        float colonyWealth = map.wealthWatcher.WealthTotal;
        Log.Message($"Richesse de la colonie : {colonyWealth}");
        //Obtenir le storyteller 
        private float AdjustRaidBasedOnStoryteller(StorytellerDef storyteller)
        {
            if (storyteller.defName == "Cassandra")
            {
                return 1.0f; // Cassandra : équilibré
            }
            else if (storyteller.defName == "Phoebe")
            {
                return 0.5f; // Phoebe : raids moins fréquents
            }
            else if (storyteller.defName == "Randy")
            {
                return 1.5f; // Randy : plus aléatoire et potentiellement plus intense
            }
            return 1.0f;
        }

        // Adapter la taille du raid en fonction de la richesse et du narrateur 
        int animalCount = (int)(AdjustAnimalCountBasedOnWealth(colonyWealth) * AdjustRaidBasedOnStoryteller(Find.Storyteller.def));
        // Liste des animaux disponibles
       animalDefs = 'NovaCube_CubeeHoney'

        // Générer les animaux pour le raid
        List<Pawn> animals = new List<Pawn>();
        foreach (string defName in animalDefs)
        {
            PawnKindDef pawnKind = PawnKindDef.Named(defName);
            if (pawnKind != null)
            {
                for (int i = 0; i < animalCount / animalDefs.Length; i++) // Répartir le nombre total d'animaux
                {
                    Pawn animal = PawnGenerator.GeneratePawn(pawnKind, null);
                    animals.Add(animal);
                }
            }
        }
            private int AdjustAnimalCountBasedOnWealth(float wealth)
    {
        // Logique pour ajuster le nombre d'animaux en fonction de la richesse
        if (wealth < 20000)
        {
            return Rand.Range(3, 6); // Colonies pauvres : petit raid
        }
        else if (wealth < 80000)
        {
            return Rand.Range(7, 15); // Colonies moyennes : raid moyen
        }
        else
        {
            return Rand.Range(15, 25); // Colonies riches : gros raid
        }
    }
}
        // Positionner les animaux sur la carte
        IntVec3 spawnSpot;
        if (!RCellFinder.TryFindRandomPawnEntryCell(map, out spawnSpot, CellFinder.EdgeRoadChance_Hostile))
        {
            Log.Warning("Impossible de trouver une position pour le raid.");
            return false;
        }


// Envoyer un message au joueur
Messages.Message($"Bzzz..", MessageTypeDefOf.ThreatBig, new TargetInfo(spawnSpot, map));
        Log.Message($"Nombre d'animaux: {animals.Count}");

return true;
    }

}
