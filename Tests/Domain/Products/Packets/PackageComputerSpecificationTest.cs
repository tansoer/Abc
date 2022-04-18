using Abc.Domain.Products;
using Abc.Domain.Products.Packets;

namespace Abc.Tests.Domain.Products.Packets {

    public sealed class PackageComputerSpecificationTest : PackageSpecificationTests {

        private ProductSet standardFeaturesSet;
        private ProductSet winMeMemorySet;
        private ProductSet winOtherMemorySet;
        private ProductSet hardDriveSet;
        private ProductSet monitorSet;
        private ProductSet videoCardSet;
        private ProductSet deviceSet;
        private ProductSet comboSet;
        private ProductSet zipSet;
        private ProductSet dvdSet;
        private ProductSet cdrwSet;
        private ProductSet soundCardSet;
        private ProductSet speakersSet;
        private ProductSet winSet;
        private ProductSet winMeSet;
        private ProductSet winOtherSet;

        private ProductType pentium2GHz;
        private ProductType floppy;
        private ProductType miniTowerChassis;
        private ProductType keyboard;
        private ProductType mouse;
        private ProductType ram256Mb;
        private ProductType ram384Mb;
        private ProductType ram512Mb;
        private ProductType ram768Mb;
        private ProductType ram1024Mb;
        private ProductType ram2048Mb;
        private ProductType hd40Gb;
        private ProductType hd40GbTurbo;
        private ProductType hd60GbTurbo;
        private ProductType hd80Gb;
        private ProductType hd120Gb;
        private ProductType monitorStandard17;
        private ProductType monitorProfessional17;
        private ProductType monitorStandard19;
        private ProductType monitorProfessional19;
        private ProductType monitorProfessional21;
        private ProductType monitorLcd15;
        private ProductType monitorLcd17;
        private ProductType monitorLcd19;
        private ProductType videoCardGeForceMx;
        private ProductType videoCardGeForceTi200;
        private ProductType videoCardGeForceTi500;
        private ProductType deviceDvd;
        private ProductType deviceComboDrive;
        private ProductType deviceZip;
        private ProductType deviceCdrw;
        private ProductType soundCardSbLive;
        private ProductType stereoSpeaker;
        private ProductType osWinMe;
        private ProductType osWinXp;
        private ProductType osWinXpPro;
        private ProductType osWin2000;

        public void Run(PackageType t) {
            createItems();
            createSets();
            addProductInclusions(t);
            validatePackages(t);
        }
        private void validatePackages(PackageType t) {
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(deviceSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winMeSet),
                getRandomProductType(winMeMemorySet));
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(deviceSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                ram768Mb);
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(deviceSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                ram1024Mb);
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(deviceSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                ram2048Mb);
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(deviceSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                getRandomProductType(winMeMemorySet));
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(deviceSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                getRandomProductType(winOtherMemorySet));
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(comboSet),
                getRandomProductType(zipSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                getRandomProductType(winOtherMemorySet));
            isCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(dvdSet),
                getRandomProductType(zipSet),
                getRandomProductType(cdrwSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                getRandomProductType(winOtherMemorySet));
            isNotCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(dvdSet),
                getRandomProductType(zipSet),
                getRandomProductType(cdrwSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winMeSet),
                ram1024Mb);
            isNotCorrectPackage(t,
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse,
                getRandomProductType(hardDriveSet),
                getRandomProductType(monitorSet),
                getRandomProductType(videoCardSet),
                getRandomProductType(comboSet),
                getRandomProductType(zipSet),
                getRandomProductType(cdrwSet),
                getRandomProductType(soundCardSet),
                getRandomProductType(speakersSet),
                getRandomProductType(winOtherSet),
                getRandomProductType(winOtherMemorySet));
        }
        private void addProductInclusions(PackageType t) {
            addProductInclusion(t, standardFeaturesSet, 5, 5);
            addProductInclusion(t, hardDriveSet, 1, 1);
            addProductInclusion(t, monitorSet, 1, 1);
            addProductInclusion(t, videoCardSet, 1, 1);
            addProductInclusion(t, deviceSet, 1, 1);
            addProductInclusion(t, soundCardSet, 1, 1);
            addProductInclusion(t, speakersSet, 1, 1);
            addProductInclusion(t, winSet, 1, 1);
            var iWinMeSet = addConditionalInclusion(t, winMeSet, 1, 1);
            addDetailInclusion(t, iWinMeSet, winMeMemorySet, 1, 1);
            var iWinOtherSet = addConditionalInclusion(t, winOtherSet, 1, 1);
            addDetailInclusion(t, iWinOtherSet, winOtherMemorySet, 1, 1);
            var iComboSet = addConditionalInclusion(t, comboSet, 1, 1);
            addDetailInclusion(t, iComboSet, zipSet, 0, 1);
            var iDvdSet = addConditionalInclusion(t, dvdSet, 1, 1);
            addDetailInclusion(t, iDvdSet, cdrwSet, 0, 1);
            addDetailInclusion(t, iDvdSet, zipSet, 0, 1);
        }
        private void createSets() {
            standardFeaturesSet = crProductSet("standardFeaturesSet",
                pentium2GHz, floppy, miniTowerChassis, keyboard, mouse);
            winMeMemorySet = crProductSet("winMEMemorySet", ram256Mb, ram384Mb, ram512Mb);
            winOtherMemorySet = crProductSet("winOtherMemorySet", ram256Mb, ram384Mb, ram512Mb
                , ram768Mb, ram1024Mb, ram2048Mb);
            hardDriveSet = crProductSet("hardDriveSet",
                hd40Gb, hd40GbTurbo, hd60GbTurbo, hd80Gb, hd120Gb);
            monitorSet = crProductSet("monitorSet",
                monitorStandard17, monitorProfessional17, monitorStandard19
                , monitorProfessional19, monitorProfessional21, monitorLcd15, monitorLcd17, monitorLcd19);
            videoCardSet = crProductSet("videoCardSet"
                , videoCardGeForceMx, videoCardGeForceTi200, videoCardGeForceTi500);
            deviceSet = crProductSet("deviceSet", deviceDvd, deviceComboDrive);
            comboSet = crProductSet("comboSet", deviceComboDrive);
            zipSet = crProductSet("zipSet", deviceZip);
            dvdSet = crProductSet("dVDSet", deviceDvd);
            cdrwSet = crProductSet("cDRWSet", deviceCdrw);
            soundCardSet = crProductSet("soundCardSet", soundCardSbLive);
            speakersSet = crProductSet("speakersSet", stereoSpeaker);
            winSet = crProductSet("winSet", osWinMe, osWinXp, osWinXpPro, osWin2000);
            winMeSet = crProductSet("winMESet", osWinMe);
            winOtherSet = crProductSet("winOtherSet", osWinXp, osWinXpPro, osWin2000);
        }
        private void createItems() {
            pentium2GHz = crProductType("pentium2GHz");
            floppy = crProductType("floppy");
            miniTowerChassis = crProductType("miniTowerChassis");
            keyboard = crProductType("keyboard");
            mouse = crProductType("mouse");
            ram256Mb = crProductType("ram256Mb");
            ram384Mb = crProductType("ram384Mb");
            ram512Mb = crProductType("ram512Mb");
            ram768Mb = crProductType("ram768Mb");
            ram1024Mb = crProductType("ram1024Mb");
            ram2048Mb = crProductType("ram2048Mb");
            hd40Gb = crProductType("hd40Gb");
            hd40GbTurbo = crProductType("hd40GbTurbo");
            hd60GbTurbo = crProductType("hd60GbTurbo");
            hd80Gb = crProductType("hd80Gb");
            hd120Gb = crProductType("hd120Gb");
            monitorStandard17 = crProductType("monitorStandard17");
            monitorProfessional17 = crProductType("monitorProfessional17");
            monitorStandard19 = crProductType("monitorStandard19");
            monitorProfessional19 = crProductType("monitorProfessional19");
            monitorProfessional21 = crProductType("monitorProfessional21");
            monitorLcd15 = crProductType("monitorLcd15");
            monitorLcd17 = crProductType("monitorLcd17");
            monitorLcd19 = crProductType("monitorLcd19");
            videoCardGeForceMx = crProductType("videoCardGeForceMx");
            videoCardGeForceTi200 = crProductType("videoCardGeForceTi200");
            videoCardGeForceTi500 = crProductType("videoCardGeForceTi500");
            deviceDvd = crProductType("deviceDvd");
            deviceComboDrive = crProductType("deviceComboDrive");
            deviceZip = crProductType("deviceZip");
            deviceCdrw = crProductType("Read-Write CD device");
            soundCardSbLive = crProductType("soundCardSbLive");
            stereoSpeaker = crProductType("stereoSpeaker");
            osWinMe = crProductType("osWinMe");
            osWinXp = crProductType("osWinXp");
            osWinXpPro = crProductType("osWinXpPro");
            osWin2000 = crProductType("osWin2000");

        }

    }

}
