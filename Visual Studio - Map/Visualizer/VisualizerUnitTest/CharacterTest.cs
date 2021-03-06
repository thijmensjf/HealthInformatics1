﻿// <copyright file="CharacterTest.cs" company="HI1">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace VisualizerUnitTest
{
    using NUnit.Framework;
    using System.Threading;
    using Visualizer;
    using Visualizer.Character;

    /// <summary>
    /// Class CharacterTest.
    /// </summary>
    [TestFixture, Apartment(ApartmentState.STA)]
    public class CharacterTest
    {
        #region Fields

        private Character character;

        #endregion Fields

        #region Methods

        [SetUp]
        public void setUp()
        {
            character = new Character(0, 0, 0, 0, "character");
        }

        /// <summary>
        /// Creates the character test.
        /// </summary>
        [Test]
        public void CreateCharacterTest()
        {
            Assert.IsNotNull(character);
        }

        [Test]
        public void setEmotionTestSucceed()
        {
            character.EmotionUpdatedTo(5);
            Assert.AreEqual(CharacterEmotion.Happy, character.CharEmotion);
        }

        [Test]
        public void setEmotionTestFail()
        {
            character.EmotionUpdatedTo(2);
            Assert.AreEqual(CharacterEmotion.Afraid, character.CharEmotion);
            character.EmotionUpdatedTo(0);
            Assert.AreEqual(CharacterEmotion.Afraid, character.CharEmotion);
        }

        [Test]
        public void setActionTestSucceed()
        {
            character.ActionUpdatedTo(5);
            Assert.AreEqual(CharacterAction.FollowUser, character.CharAction);
        }

        [Test]
        public void setActionTestFail()
        {
            character.ActionUpdatedTo(2);
            Assert.AreEqual(CharacterAction.CallingPhone, character.CharAction);
            character.ActionUpdatedTo(0);
            Assert.AreEqual(CharacterAction.CallingPhone, character.CharAction);
        }

        /// <summary>
        /// Gets the character resource test.
        /// </summary>
        [Test]
        public void GetCharacterResourceTest()
        {
            string actualString = character.GetImageResource(0);
            string shouldBeString = ImageEnum.GetCharacter(0);
            Assert.AreEqual(shouldBeString, actualString);
        }

        /// <summary>
        /// Gets the character emotion resource test.
        /// </summary>
        ///
        [Test]
        public void GetCharacterEmotionResourceTest()
        {
            string actualString = character.GetImageEmotionResource(0);
            string shouldBeString = ImageEnum.GetCharacterEmotion(0);
            Assert.AreEqual(shouldBeString, actualString);
        }

        #endregion Methods
    }
}