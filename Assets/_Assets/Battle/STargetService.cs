using System.Collections.Generic;
public interface STargetService
{
    public List<SBattleCharacter> GetTargetsForTeam(int teamid, bool hostiletarget);
    public STargetingComponent GetTargetingComponent();
}
