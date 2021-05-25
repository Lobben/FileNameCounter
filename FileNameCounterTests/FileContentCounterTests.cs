using FileNameCounter;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FileNameCounterTest
{
    public class FileContentCounterTests
    {
        [Fact]
        public void GetOccurrencesInFile_OneRowOneOccurence_ReturnsOne()
        {
            // Arrange
            var path = "C:/path/file.txt";
            var toLookFor = "file";
            var contentRow = "file";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string> { contentRow });

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetOccurrencesInFile_OneRowTwoOccurence_ReturnsTwo()
        {
            // Arrange
            var path = "C:/path/file.txt";
            var toLookFor = "file";
            var contentRow = "filefile";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string> { contentRow });

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetOccurrencesInFile_TwoRowsOneMatch_ReturnsOne()
        {
            // Arrange
            var path = "C:/path/file.txt";
            var toLookFor = "file";
            var contentRow1 = "file";
            var contentRow2 = "fil";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string> { contentRow1, contentRow2 });

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetOccurrencesInFile_TwoRowsThreeMatches_ReturnsThree()
        {
            // Arrange
            var path = "C:/path/file.txt";
            var toLookFor = "file";
            var contentRow1 = "file";
            var contentRow2 = "filefile";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string> { contentRow1, contentRow2 });

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetOccurrencesInFile_ThreeOverlappingOccurences_ReturnsThree()
        {
            // Arrange
            var path = "C:/path/010.txt";
            var toLookFor = "010";
            var contentRow = "0101010";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string> { contentRow });

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetOccurrencesInFile_ThreeDiffentCasedOccurences_ReturnsThree()
        {
            // Arrange
            var path = "C:/path/010.txt";
            var toLookFor = "File";
            var contentRow = "fileFILEfIlE";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string> { contentRow });

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetOccurrencesInFile_NoOccurences_ReturnsZero()
        {
            // Arrange
            var path = "C:/path/file.txt";
            var toLookFor = "file";
            var contentRow = "ffiillee";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string> { contentRow });

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetOccurrencesInFile_NoContent_ReturnsZero()
        {
            // Arrange
            var path = "C:/path/file.txt";
            var toLookFor = "file";

            var fileHelperMock = new Mock<IFileHandler>();
            fileHelperMock
                .Setup(x => x.ReadLines(path))
                .Returns(new List<string>());

            var fileContentCounter = new FileContentCounter(fileHelperMock.Object);

            // Act
            var result = fileContentCounter.GetOccurrencesInFile(path, toLookFor);

            // Assert
            Assert.Equal(0, result);
        }
    }
}
