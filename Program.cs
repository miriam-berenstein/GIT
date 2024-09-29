using GIT;

GITUser user1 = new("Shira");
user1.AddBranch("DesignPatterns");
user1.GetBranch("DesignPatterns").Add(new MyFile("firstFile", "Shira", "this is my content"));
user1.GetBranch("DesignPatterns").Add(new Folder("firstFolder", "Miriam"));
user1.GetBranch("DesignPatterns").Add(new Branch("SecondBranch", "Shira"));
user1.GetBranch("SecondBranch").Add(new Folder("firstSecondaryFolder", "Miriam"));
((Folder)user1.GetBranch("SecondBranch").Children["firstSecondaryFolder"]).Add(new MyFile("FirstThirdFile", "Shira"));
user1.GetBranch("SecondBranch").Add(new MyFile("firstSecondaryFile", "Miriam"));
user1.print();
user1.GetBranch("DesignPatterns").Clone("copyOfDesignPatterns");
((Folder)user1.GetBranch("DesignPatterns").Children["firstSecondaryFolder"]).Children["FirstThirdFile"].Merge(new MyFile("MergedFile", "Miriam", "I changed my text!"));
Console.WriteLine("--------------");
user1.print();
GITUser user2 = new("Miriam");
Console.WriteLine("hi");