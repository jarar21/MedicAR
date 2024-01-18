using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextWriter : MonoBehaviour
{
    public string text;
    public TMP_Text textfield;
    public float writingspeed;
    public Dictionary<string, string> DataHashTable = new Dictionary<string, string>();
    public Canvas canvas;
    private void Awake()
    {
        //filling Brain data in the hash table

        DataHashTable["brain"] = "The human brain is the central organ of the nervous system, responsible for processing sensory information, regulating bodily functions, and facilitating cognition. It governs activities ranging from basic life-sustaining processes like breathing and heartbeat to complex cognitive functions such as thought, memory, decision-making, and emotional responses. As the epicenter of consciousness, the brain plays a pivotal role in determining our perceptions, actions, and overall human experience.";
        DataHashTable["frontallobe"] = "The frontal lobe, located at the front of the cerebral cortex, is a critical region of the human brain responsible for a multitude of higher cognitive functions. It plays a central role in executive functions, including decision-making, problem-solving, and the regulation of social behavior. Additionally, the frontal lobe is intricately involved in motor control, personality expression, and aspects of language comprehension. This region undergoes significant development during adolescence, influencing aspects of self-control and goal-oriented behaviors. Dysfunction in the frontal lobe can result in various cognitive and behavioral impairments, emphasizing its crucial role in shaping human thought processes and actions.";
        DataHashTable["parietallobe"] = "The parietal lobe is a crucial region of the human brain located near the top and back, contributing to sensory processing and spatial awareness. Responsible for integrating sensory information from various sources, the parietal lobe plays a key role in functions like perception of touch, pressure, temperature, and spatial orientation. Additionally, it is involved in higher cognitive processes such as attention, navigation, and the understanding of spatial relationships. The parietal lobe's intricate network allows individuals to interpret their surroundings, interact with the environment, and navigate the world effectively.";
        DataHashTable["temporallobe"] = "The temporal lobe is a crucial region in the human brain, located on each side above the ears. Responsible for processing auditory stimuli, the temporal lobe is integral to our ability to hear and comprehend language. Additionally, it plays a key role in memory formation and is associated with the recognition of faces and objects. Beyond sensory functions, the temporal lobe is intricately linked to emotional responses, facilitating our ability to experience and interpret emotions. As part of the broader brain network, the temporal lobe contributes significantly to shaping our perceptions, memories, and overall human experience.";
        DataHashTable["occipitallobe"] = "The occipital lobe, positioned at the posterior region of the brain, is primarily responsible for processing visual information. As a vital component of the cerebral cortex, the occipital lobe interprets signals received from the eyes, allowing individuals to perceive and comprehend the surrounding visual environment. Its intricate neural networks enable the recognition of shapes, colors, and spatial relationships, contributing significantly to the complex cognitive functions orchestrated by the human brain.";
        DataHashTable["cerebellum"] = "The cerebellum, situated at the back of the brain beneath the cerebral hemispheres, is a crucial component of the human brain, contributing significantly to motor control, coordination, and balance. Though it comprises only a small fraction of the brain's mass, the cerebellum contains an intricate network of neurons and is essential for the smooth execution of voluntary movements. In addition to its primary role in motor functions, emerging research suggests that the cerebellum also plays a role in cognitive processes, contributing to aspects of attention, language, and emotional regulation. Its intricate connections with other brain regions highlight its multifaceted significance in shaping both physical and cognitive aspects of human experience.";
        DataHashTable["spinalchord"] = "The spinal cord, an extension of the central nervous system, serves as a crucial conduit between the brain and the peripheral nervous system. Protected by the vertebral column, the spinal cord facilitates the transmission of signals to and from the brain, enabling the coordination of various motor and sensory functions. It plays a fundamental role in reflex actions and serves as a vital pathway for information exchange between different parts of the body and the brain. Through its intricate network of nerves, the spinal cord contributes significantly to our ability to move, perceive sensations, and respond to external stimuli, exemplifying its indispensable role in the overall functioning of the human nervous system.";


        //filling Heart data in the hash table

        DataHashTable["heart"] = "The human heart is a vital organ responsible for pumping blood throughout the body, ensuring the delivery of oxygen and essential nutrients to tissues while simultaneously removing waste products like carbon dioxide. Divided into four chambers, it effectively separates oxygenated and deoxygenated blood, adjusts its contraction rate to meet the body's needs, and plays a role in regulating blood pressure through hormonal release.";
        DataHashTable["svc"] = "The superior vena cava is a major blood vessel that carries deoxygenated blood from the upper part of the body back to the right atrium of the heart. Situated above the heart, it collects blood from the head, neck, arms, and upper torso, channeling it into the right atrium where it is then pumped into the pulmonary circulation for oxygenation. As a crucial component of the cardiovascular system, the superior vena cava ensures the continuous circulation of blood, supporting vital physiological processes throughout the human body.";
        DataHashTable["aorta"] = "The aorta is the largest artery in the human body, originating from the left ventricle of the heart. Functioning as the main vessel of the systemic circulatory system, it carries oxygenated blood to various organs and tissues. The aorta is divided into segments, including the ascending aorta, aortic arch, and descending aorta, each serving specific regions. Its elastic properties allow it to withstand the force of blood ejected from the heart, and it plays a crucial role in maintaining blood pressure and ensuring the distribution of oxygen-rich blood throughout the body. The aorta is essential for sustaining life, facilitating the continuous circulation of blood that supports bodily functions and physiological processes.";
        DataHashTable["pulmonaryartery"] = "The pulmonary artery is a major blood vessel connected to the heart, specifically emerging from the right ventricle. It serves a crucial role in the circulatory system by carrying deoxygenated blood from the heart to the lungs, where oxygen exchange occurs. The pulmonary artery plays a vital part in the pulmonary circulation, facilitating the oxygenation of blood before it is returned to the heart to be pumped throughout the body. This process is fundamental to sustaining life and ensuring the delivery of oxygen to various tissues and organs.";
        DataHashTable["rightartrium"] = "The pulmonary artery is a major blood vessel connected to the heart, specifically emerging from the right ventricle. It serves a crucial role in the circulatory system by carrying deoxygenated blood from the heart to the lungs, where oxygen exchange occurs. The pulmonary artery plays a vital part in the pulmonary circulation, facilitating the oxygenation of blood before it is returned to the heart to be pumped throughout the body. This process is fundamental to sustaining life and ensuring the delivery of oxygen to various tissues and organs.";
        DataHashTable["leftartrium"] = "The left atrium is one of the four chambers of the human heart, serving as a crucial component of the circulatory system. Situated on the left side, this chamber receives oxygenated blood from the lungs through the pulmonary veins. During the cardiac cycle, the left atrium contracts to propel this oxygen-rich blood into the left ventricle, initiating the process of systemic circulation. Working in tandem with the left ventricle, the left atrium contributes to maintaining the continuous flow of oxygenated blood throughout the body, ensuring essential organ functions and sustaining overall cardiovascular health.";

        //filling Liver data in the hash table

        DataHashTable["gallbladder"] = "The gallbladder is a small, pear-shaped organ located beneath the liver in the human body. Its primary function is to store and concentrate bile, a digestive fluid produced by the liver, before releasing it into the small intestine to aid in the digestion and absorption of fats. Bile is essential for breaking down fats into smaller particles, facilitating their absorption by the body. While the gallbladder plays a role in digestion, it is not indispensable, and individuals can lead normal lives even after its removal through a surgical procedure known as cholecystectomy. Issues such as gallstones or inflammation may necessitate the removal of the gallbladder, but its absence typically does not severely impact digestive functions.";
        DataHashTable["rightlobe"] = "The right lobe of the liver, an integral part of the human digestive system, is located on the right side of the abdomen beneath the ribcage. This vital organ is responsible for numerous functions, including the metabolism of nutrients, detoxification of harmful substances, and the production of bile to aid in digestion. Working in harmony with the left lobe, the right lobe contributes to the body's overall physiological balance, emphasizing the liver's crucial role in maintaining health and supporting various metabolic processes essential for the body's well-being.";
        DataHashTable["falciformligament"] = "The falciform ligament of the liver is a thin, flat ligament that attaches the liver to the anterior abdominal wall and the diaphragm. It is a fold of peritoneum that divides the liver into right and left lobes, with the ligament serving as a supportive structure for the liver's position within the abdominal cavity. This ligament also contains the ligamentum teres, which is a remnant of the umbilical vein from fetal development. The falciform ligament contributes to the overall structural integrity of the liver and aids in maintaining its anatomical position within the body.";
        DataHashTable["leftlobe"] = "The left lobe of the liver, a vital organ located in the upper right abdomen, is one of the two main lobes that comprise the liver. This asymmetrical organ performs crucial functions, including filtering blood from the digestive tract before passing it to the rest of the body and metabolizing nutrients, medications, and toxins. Additionally, the liver plays a key role in producing essential proteins, storing glycogen, and regulating blood sugar levels. Its complex structure and multifaceted functions make the left lobe of the liver indispensable for maintaining overall health and metabolic balance within the human body.";
        DataHashTable["ivc"] = "The inferior vena cava (IVC) is a large vein that carries deoxygenated blood from the lower part of the body back to the heart. In the context of the liver, the IVC plays a crucial role in hepatic circulation. Blood from the hepatic veins, which drain the liver, ultimately flows into the IVC, ensuring the return of blood to the right atrium of the heart. This vascular pathway is vital for maintaining proper blood circulation and supporting the liver's essential functions, including nutrient processing and detoxification.";


        //filling Lung data in hash table

        DataHashTable["trachea"] = "The trachea, commonly known as the windpipe, is a vital component of the respiratory system in humans. It serves as a flexible tube connecting the larynx, or voice box, to the bronchi, facilitating the passage of air to and from the lungs. The trachea is comprised of cartilage rings that provide structural support, preventing collapse during breathing. Ciliated cells lining the trachea help trap and remove foreign particles, contributing to respiratory hygiene. As part of the intricate network of airways, the trachea plays a crucial role in ensuring the flow of oxygen to the lungs and the elimination of carbon dioxide, supporting the fundamental physiological process of respiration.";
        DataHashTable["bronchi"] = "The bronchi are the main air passages in the lungs, branching from the trachea and extending into the left and right lungs. Their primary function is to transport air between the trachea and the smaller bronchioles, facilitating the exchange of oxygen and carbon dioxide during respiration. The bronchi further divide into smaller bronchioles, forming a tree-like structure that delivers air to the alveoli, where gas exchange occurs. This intricate network ensures the distribution of oxygen to the bloodstream and the removal of carbon dioxide, supporting the essential physiological process of breathing.";
        DataHashTable["leftlobe"] = "The left lobe of the lungs, an integral component of the respiratory system, functions in conjunction with the right lobe to facilitate the exchange of oxygen and carbon dioxide during the process of breathing. Comprising lobes that are further divided into smaller units called lobules, the left lung accommodates the heart's presence in the chest cavity. It plays a vital role in sustaining life by ensuring a continuous flow of oxygen into the bloodstream, supporting various physiological processes, and maintaining the delicate balance necessary for overall respiratory function.";
        DataHashTable["rightlobe"] = "The right lobe of the lungs, an integral component of the respiratory system, complements its counterpart on the left side to facilitate the exchange of oxygen and carbon dioxide during the process of breathing. Comprised of lobes, bronchi, and intricate networks of airways, the right lung functions to ensure the vital exchange of gases required for sustaining life. Its anatomical structure and coordination with the left lung contribute to the overall respiratory efficiency, allowing the body to meet its oxygenation needs while expelling waste gases. The right lung, like the left, plays an indispensable role in maintaining the delicate balance necessary for the body's physiological processes and overall well-being.";


        //filling Kidney data in hash table

        DataHashTable["fibrouscapsule"] = "The fibrous capsule of the kidney is a protective layer surrounding each kidney, contributing to the organ's structural integrity. Composed of connective tissue, the fibrous capsule encases the kidney, shielding it from external forces and providing support. While the capsule is durable, its pliability allows for the kidney's essential functions, such as filtration and blood flow regulation. This protective covering maintains the kidney's position within the body and assists in safeguarding its vital role in maintaining fluid balance and eliminating waste products.";
        DataHashTable["renalartery"] = "The renal artery, a vital component of the human circulatory system, supplies oxygenated blood to the kidneys, ensuring their proper function in filtering waste and maintaining fluid balance. Arising from the abdominal aorta, the renal artery branches into smaller vessels within the kidneys, facilitating the intricate process of blood filtration and the removal of metabolic byproducts. This essential vascular network plays a crucial role in supporting overall renal health and systemic homeostasis.";
        DataHashTable["renalvein"] = "The renal vein is a vital component of the kidney's circulatory system, responsible for draining deoxygenated blood from the renal capillaries and transporting it back to the heart. Emerging from the renal hilum, the renal vein carries blood enriched with waste products, such as urea and excess electrolytes, towards the inferior vena cava, ultimately participating in the larger systemic circulation. This crucial process allows for the efficient removal of metabolic byproducts from the kidneys, contributing to overall homeostasis within the body.";
        DataHashTable["ureter"] = "The ureter is a muscular tube that connects each kidney to the urinary bladder, facilitating the transport of urine from the kidneys to the bladder. Essential for the urinary system, the ureters ensure the unidirectional flow of urine, preventing backflow. The walls of the ureter consist of smooth muscle that contracts rhythmically to propel urine downward through peristaltic movements. Valves at the junctions of the ureters and bladder help maintain urinary tract integrity. The efficient functioning of the ureters is crucial for proper excretion, maintaining fluid balance, and eliminating waste products from the body.";


    }
    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
    public void WriteText(string index)
    {
        textfield.text = "";
        StartCoroutine(NewTextWriter(index));
    }
    public void WriteOrganComponentData(string key)
    {
        textfield.text = "";
        
        string toDisplay = DataHashTable[key] ;
        Debug.Log(toDisplay);
        StopAllCoroutines();

        StartCoroutine(NewTextWriter(toDisplay));
    }
    IEnumerator NewTextWriter(string index)
    {
        foreach (char c in index.ToCharArray())
        {
            textfield.text += c;
            yield return new WaitForSeconds(writingspeed);
        }
    }
    private void OnEnable()
    {
        textfield.text = "";
        
        WriteText(text);
    }
    private void OnDisable()
    {
       StopCoroutine(NewTextWriter(text));
    }
}
