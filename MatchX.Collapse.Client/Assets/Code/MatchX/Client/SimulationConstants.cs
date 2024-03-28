namespace MatchX.Client
{

	public static class SimulationConstants
	{
		public const int ClientTargetFrameRate = 60;
		public const int EngineTargetFrameRate = 24;
			
		public const float ClientTargetDeltaTime = 1f / ClientTargetFrameRate;
		public const float EngineTargetDeltaTime = 1f / EngineTargetFrameRate;

		public const float Gravity = -9.8f;
	}

}