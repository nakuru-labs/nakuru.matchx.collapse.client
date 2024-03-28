namespace MatchX.Common.Services
{

	public abstract partial class CommonServices
	{
		public static class Timing
		{
			public static float TicksToSeconds(int ticks, float targetDeltaTime)
			{
				return ticks * targetDeltaTime;
			}
			
			public static int SecondsToTicks(int seconds, int targetFrameRate)
			{
				return seconds * targetFrameRate;
			}
		}
	}
}