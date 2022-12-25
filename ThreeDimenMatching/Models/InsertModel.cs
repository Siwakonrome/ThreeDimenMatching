using System.Collections.Generic;

namespace ThreeDimenMatching.Models
{
    class InsertModel
    {
        public string modelName { get; set; }
        public float score { get; set; }
        public Pose pickOffset { get; set; }
        public string camParams { get; set; }
        public Pose captureOnStation { get; set; }
        public List<Pose> pushOnStation { get; set; }
        public Pose captureOnTray { get; set; }
        public List<Pose> pushOnTray { get; set; }
        public List<Joints> fixPosStation { get; set; }
        public List<Joints> fixPosTray { get; set; }
        public string gripperId { get; set; }
    }
}














