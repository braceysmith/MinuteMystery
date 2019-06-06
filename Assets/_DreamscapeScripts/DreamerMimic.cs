		using 
		UnityEngine									;

		public 
				class 
		DreamerMimic 								: 
					MonoBehaviour					{
													[
		SerializeField								]
		private 
		GameObject 
					anja							;

							// I want to move like she does, By Bracey Smith

		void  
			Update 									(
													) 
													{
		this										.
			transform								.
					rotation						= 
		anja										.
			transform								.
					rotation						;
													}
													}
