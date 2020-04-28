import argparse


class ArgumentParser:

    def __init__(self):
        self.parser = argparse.ArgumentParser(
            formatter_class=argparse.RawTextHelpFormatter,
            description='Lodz University of Technology (TUL)'
                        '\nEvolutionary Computation (Obliczenia Ewolucyjne)'
                        '\n\nTask 2 - Multiobjective optimization'
                        '\n\nAuthors:'
                        '\n  Bartosz Jurczewski\t234067'
                        '\n  Karol Podlewski\t234106')

        self.parser.add_argument(metavar='ALGORITHM', dest='algorithm',
                                 type=int, choices=range(1, 4),
                                 help='Algorithm:\n  [1] NSGA-II'
                                      '\n  [2] SPEA-II\n  [3] PESA-II')

        self.parser.add_argument(metavar='TEST_PROBLEM', dest='test_problem',
                                 type=int, choices=range(1, 4),
                                 help='Test problem:\n  [1] ZDT1'
                                      '\n  [2] ZDT2\n  [3] ZDT3')

        self.parser.add_argument('-g', dest='generations',
                                 type=int, default=10000,
                                 help='Number of generations (default 10 000)')

        self.parser.add_argument('-p', dest='population',
                                 type=int, default=100,
                                 help='Population size (default 100)')

        self.parser.add_argument('-r', dest='probability',
                                 type=float, default=0.5,
                                 help='Probability (default 0.5)')

        self.parser.add_argument('-e', dest='extra',
                                 type=int, default=None,
                                 help='Extra parameter (default None)')

        self.parser.add_argument('--screen', dest='print_on_screen', 
                                 action='store_const', const=True, default=False,
                                 help='Print result also on screen (by default'
                                      'results are saved to file)')

        self.args = self.parser.parse_args()


    def get_arguments(self):
        return self.args
