using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities.Enums
{
    [Flags]
    public enum ExpertiseType : long
    {
        // 📘 Abstract / Category-level
        // Astrology = 1 << 0,
        // Numerology = 1 << 1,
        // Tarot = 1 << 2,
        // Palmistry = 1 << 3,
        // Psychic = 1 << 4,
        // Healing = 1 << 5,
        // Vastu = 1 << 6,
        // Consultation = 1 << 7,
        // Rituals = 1 << 8,
        // KundliServices = 1 << 9,
        
        [Display(Name = "None")]
        None = 0,

        // Kundli Types
        [Display(Name = "Janma Kundli (Birth Chart)")]
        JanmaKundli = 1 << 0,

        [Display(Name = "Lagna Kundli")]
        LagnaKundli = 1 << 1,

        [Display(Name = "Chandra Kundli (Moon Chart)")]
        ChandraKundli = 1 << 2,

        [Display(Name = "Navamsa Kundli (D-9)")]
        NavamsaKundli = 1 << 3,

        [Display(Name = "Chalit Kundli")]
        ChalitKundli = 1 << 4,

        [Display(Name = "Ashtakvarga Chart")]
        AshtakvargaChart = 1 << 5,

        [Display(Name = "Dashamsa Kundli (D-10)")]
        DashamsaKundli = 1 << 6,

        [Display(Name = "Shodasvarga Charts")]
        ShodasvargaCharts = 1 << 7,

        [Display(Name = "Prashna Kundli (Question Chart)")]
        PrashnaKundli = 1 << 8,

        [Display(Name = "Kundli Matching")]
        KundliMatching = 1 << 9,

        [Display(Name = "Varshaphal Kundli (Annual)")]
        VarshaphalKundli = 1 << 10,

        // Astrology & Related
        [Display(Name = "Horoscope Reading")]
        HoroscopeReading = 1 << 11,

        [Display(Name = "Vedic Astrology")]
        VedicAstrology = 1 << 12,

        [Display(Name = "Western Astrology")]
        WesternAstrology = 1 << 13,

        [Display(Name = "Career Astrology")]
        CareerAstrology = 1 << 14,

        [Display(Name = "Matchmaking")]
        Matchmaking = 1 << 15,

        [Display(Name = "Auspicious Timing")]
        AuspiciousTiming = 1 << 16,

        // Numerology
        [Display(Name = "Numerology Reading")]
        NumerologyReading = 1 << 17,

        [Display(Name = "Name Correction")]
        NameCorrection = 1 << 18,

        [Display(Name = "Lucky Numbers")]
        LuckyNumbers = 1 << 19,

        // Tarot & Cards
        [Display(Name = "Tarot Card Reading")]
        TarotCardReading = 1 << 20,

        [Display(Name = "Angel Card Reading")]
        AngelCardReading = 1 << 21,

        [Display(Name = "Oracle Card Reading")]
        OracleCardReading = 1 << 22,

        // Palm & Face Reading
        [Display(Name = "Palm Reading")]
        PalmReading = 1 << 23,

        [Display(Name = "Face Reading")]
        FaceReading = 1 << 24,

        // Psychic & Mind
        [Display(Name = "Mind Reading")]
        MindReading = 1 << 25,

        [Display(Name = "Psychic Reading")]
        PsychicReading = 1 << 26,

        [Display(Name = "Intuitive Guidance")]
        IntuitiveGuidance = 1 << 27,

        [Display(Name = "Dream Interpretation")]
        DreamInterpretation = 1 << 28,

        [Display(Name = "Past Life Reading")]
        PastLifeReading = 1 << 29,

        [Display(Name = "Spirit Communication")]
        SpiritCommunication = 1 << 30,

        // Healing & Energy Work
        [Display(Name = "Reiki Healing")]
        ReikiHealing = 1L << 31,

        [Display(Name = "Chakra Balancing")]
        ChakraBalancing = 1L << 32,

        [Display(Name = "Aura Reading")]
        AuraReading = 1L << 33,

        [Display(Name = "Crystal Healing")]
        CrystalHealing = 1L << 34,

        [Display(Name = "Energy Cleansing")]
        EnergyCleansing = 1L << 35,

        // Vastu & Space
        [Display(Name = "Vastu Consultation")]
        VastuConsultation = 1L << 36,

        [Display(Name = "Feng Shui")]
        FengShui = 1L << 37,

        [Display(Name = "Property Selection")]
        PropertySelection = 1L << 38,

        // Life Consultations
        [Display(Name = "Health Consultation")]
        HealthConsultation = 1L << 39,

        [Display(Name = "Financial Advice")]
        FinancialAdvice = 1L << 40,

        [Display(Name = "Relationship Support")]
        RelationshipSupport = 1L << 41,

        [Display(Name = "Legal Consultation")]
        LegalConsultation = 1L << 42,

        [Display(Name = "Education Guidance")]
        EducationGuidance = 1L << 43,

        // Rituals & Remedies
        [Display(Name = "Gemstone Recommendation")]
        GemstoneRecommendation = 1L << 44,

        [Display(Name = "Mantra Remedies")]
        MantraRemedies = 1L << 45,

        [Display(Name = "Pooja Advice")]
        PoojaAdvice = 1L << 46,

        [Display(Name = "Yantra Suggestion")]
        YantraSuggestion = 1L << 47,

        [Display(Name = "Totke Remedies")]
        TotkeRemedies = 1L << 48
    }

}