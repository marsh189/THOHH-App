using NodeInspector;


namespace PaintCraft.Tools.Filter{
	/// <summary>
	/// Remove duplicate last input positions.
	/// if we received the same position from Input. just remove last so we will have just one point with that position
	/// </summary>
    [NodeMenuItem("ChangePoint/RemoveDuplicateLastInputPositions")]
    public class RemoveDuplicateLastInputPositions : FilterWithNextNode {
		#region implemented abstract members of FilterWithNextNode

		public override bool FilterBody (BrushContext brushLineContext)
		{
			if (brushLineContext.Points.Count > 1){
				if (brushLineContext.Points.Last.Value.Position == brushLineContext.Points.Last.Previous.Value.Position){
					brushLineContext.ForceRemoveLastNodePoint();
					return false;
				}
			}
			return true;
		}

		#endregion



	}
}